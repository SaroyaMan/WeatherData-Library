using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Weather
{
	/// <summary>
	/// Represents a REStful web service of http://www.openweathermap.org and it implements
	/// the inteface IWeatherDataService - means it responsible to create a WeatherData object with
	/// all data according to this web service. This class implements the singleton design pattern
	/// </summary>
	public class OpenWeatherMapDataService : IWeatherDataService
	{
		private const string Key = "25e33a5538d523dbbc2f13a08c0cbd4c";
		private const string Format = "&type=accurate&units=metric&mode=xml&appid=";
		private readonly StringBuilder UrlXmlAddress;
		/// <summary>
		/// A static reference to the single object (a part of singleton design pattern)
		/// </summary>
		private static OpenWeatherMapDataService instance;

		/// <summary>
		/// A private constructor to ensure no one can access it.
		/// </summary>
		private OpenWeatherMapDataService() { UrlXmlAddress = new StringBuilder();}

		/// <summary>
		/// Retrieving the singleton instance (lazy initialization).
		/// </summary>
		/// <returns>The single object of OpenWeatherMapDataService</returns>
		public static OpenWeatherMapDataService GetInstance()
		{
			return instance ?? (instance = new OpenWeatherMapDataService());
		}

		/// <summary>
		/// Responsible to complete the url link and calls to method who parse the XML to a WeatherData
		/// object with all details.
		/// </summary>
		/// <param name="location"></param>
		/// <exception cref="NullReferenceException">Thrown when sent null to method.</exception>
		/// <exception cref="WeatherDataServiceException">
		/// Thrown when method fail to initialize Location and Weatherdata from XML.
		/// </exception>
		/// <returns>WeatherData object with all relevant information</returns>
		public WeatherData GetWeatherData(Location location)
		{
			if (location == null)
				throw new NullReferenceException("location argument can't be empty");
			if( location.IsInitialized)
				throw new WeatherDataServiceException("You can't send an initialized-location!");
			UrlXmlAddress.Append("http://api.openweathermap.org/data/2.5/weather?");
			if (location.Latitude != -1 && location.Longitude != -1)
				UrlXmlAddress.Append("lat="+ location.Latitude + "&lon="+ location.Longitude+ Format + Key);
			else
				UrlXmlAddress.Append("q=" + location.City + Format + Key);
			return ParseXmlToWeatherData(location);
		}

		/// <summary>
		/// Responsible to parse the XML and translates it to Location and WeatherData objects.
		/// It also initialize the location object with all relevant details.
		/// </summary>
		/// <param name="location"></param>
		/// <exception cref="WeatherDataServiceException">
		/// Thrown when method fail to initialize Location and Weatherdata from XML.</exception>
		/// <returns>WeatherData object with all relevant information</returns>
		private WeatherData ParseXmlToWeatherData(Location location)
		{
			WebClient client = null;
			try
			{
				client = new WebClient();
				string xml = @client.DownloadString(UrlXmlAddress.ToString());
				XDocument doc = XDocument.Parse(xml);
				location.Initialize	//fill out all missing data of location object
				(
					doc.Descendants("city").Attributes("name").First().Value,
					double.Parse(doc.Descendants("coord").Attributes("lon").First().Value),
					double.Parse(doc.Descendants("coord").Attributes("lat").First().Value),
					doc.Descendants("country").First().Value
				);
				return new WeatherData	//return WeatherData object with full data inside
				(
					location,
					double.Parse(doc.Descendants("temperature").Attributes("value").First().Value),
					double.Parse(doc.Descendants("humidity").Attributes("value").First().Value),
					double.Parse(doc.Descendants("pressure").Attributes("value").First().Value),
					doc.Descendants("clouds").Attributes("name").First().Value,
					doc.Descendants("lastupdate").Attributes("value").First().Value,
					double.Parse(doc.Descendants("speed").Attributes("value").First().Value)*3.6,
					doc.Descendants("speed").Attributes("name").First().Value,
					doc.Descendants("direction").Attributes("name").First().Value
				);
			}
			catch (WebException)
			{
				throw new WeatherDataServiceException("Faild to connect to openweathermap.org");
			}
			catch (XmlException)
			{
				throw new WeatherDataServiceException("Failed to find the XML document");
			}
			catch (InvalidOperationException)
			{
				throw new WeatherDataServiceException("Failed to parse the XML");
			}
			catch (FormatException)
			{
				throw new WeatherDataServiceException("Failed to format the string to a number");
			}
			catch(Exception e)
			{
				throw new WeatherDataServiceException(e.Message);
			}
			finally
			{
				//Closing the connection to openweathermap.org
				client?.Dispose();

				//Restarting the address so next time we also get a correct url address
				UrlXmlAddress.Clear();
			}
		}
	}
}