using System;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Weather
{
	/// <summary>
	/// Represents a REStful web service of http://www.apixu.com and it implements
	/// the inteface IWeatherDataService - means it responsible to create a WeatherData object with
	/// all data according to this web service. This class implements the singleton design pattern
	/// </summary>
	public class ApixuDataService : IWeatherDataService
	{
		private const string Key = "1eb896fa6a2a4ebbac0102920161709";
		private readonly StringBuilder UrlJsonAddress;
		/// <summary>
		/// A static reference to the single object (a part of singleton design pattern)
		/// </summary>
		private static ApixuDataService instance;

		/// <summary>
		/// A private constructor to ensure no one can access it.
		/// </summary>
		private ApixuDataService() { UrlJsonAddress = new StringBuilder();}

		/// <summary>
		/// Retrieving the singleton instance (lazy initialization).
		/// </summary>
		/// <returns>The single object of ApixuDataService</returns>
		public static ApixuDataService GetInstance()
		{
			return instance ?? (instance = new ApixuDataService());
		}

		/// <summary>
		/// Responsible to complete the url link and calls to method who parse the Json to a WeatherData
		/// object with all details.
		/// </summary>
		/// <param name="location"></param>
		/// <exception cref="NullReferenceException">Thrown when sent null to method.</exception>
		/// <exception cref="WeatherDataServiceException">
		/// Thrown when method fail to initialize Location and Weatherdata from Json.
		/// </exception>
		/// <returns>WeatherData object with all relevant information</returns>
		public WeatherData GetWeatherData(Location location)
		{
			if (location == null)
				throw new NullReferenceException("location argument can't be empty");
			if (location.IsInitialized)
				throw new WeatherDataServiceException("You can't send an initialized-location!");
			UrlJsonAddress.Append("http://api.apixu.com/v1/current.json?key=" + Key + "&q=");
			if (location.Latitude != -1 && location.Longitude != -1)
				UrlJsonAddress.Append(location.Latitude + "," + location.Longitude);
			else
				UrlJsonAddress.Append(location.City);
			return ParseJsonToWeatherData(location);
		}

		/// <summary>
		/// Responsible to parse the Json and translates it to Location and WeatherData objects.
		/// It also initialize the location object with all relevant details.
		/// </summary>
		/// <param name="location"></param>
		/// <exception cref="WeatherDataServiceException">
		/// Thrown when method fail to initialize Location and Weatherdata from Json.</exception>
		/// <returns>WeatherData object with all relevant information</returns>
		private WeatherData ParseJsonToWeatherData(Location location)
		{
			WebClient client = null;
			const string windDescription = "degree is ";
			try
			{
				client = new WebClient();
				string json = @client.DownloadString(UrlJsonAddress.ToString());
				JObject jsonObject = JObject.Parse(json);
				location.Initialize
				(
					(string)jsonObject["location"]["name"],
					double.Parse((string)jsonObject["location"]["lon"]),
					double.Parse((string) jsonObject["location"]["lat"]),
					(string) jsonObject["location"]["country"]
				);
				return new WeatherData
				(
					location,
					double.Parse((string) jsonObject["current"]["temp_c"]),
					double.Parse((string) jsonObject["current"]["humidity"]),
					double.Parse((string) jsonObject["current"]["pressure_mb"]),
					((string) jsonObject["current"]["condition"]["text"]).Remove(((string) jsonObject["current"]["condition"]["text"]).Length-1),
					(string) jsonObject["current"]["last_updated"],
					double.Parse((string) jsonObject["current"]["wind_kph"]),
					windDescription.Insert(windDescription.Length,(string) jsonObject["current"]["wind_degree"]),
					(string) jsonObject["current"]["wind_dir"]
				);
			}
			catch(WebException)
			{
				throw new WeatherDataServiceException("Faild to connect to www.apixu.com");
			}
			catch(FormatException)
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
				UrlJsonAddress.Clear();
			}
		}
	}
}