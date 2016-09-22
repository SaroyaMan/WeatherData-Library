using System;
using Weather;

/** 
 * This library has been made to developers who wants to get easly the current Weather in different
 * places around the world.
 * This short application demonstrates the usage of Weather library.
 * 
 * @category: utility library.
 * @last update: 16/09/2016.
 * @authors: Yoav Saroya - 304835887 & Sharon Yosef - 305345118.
 */
namespace TEST
{
	internal class WeatherDemo
	{
		public static void Main()
		{
			try
			{
				IWeatherDataService service = WeatherDataServiceFactory.
					GetWeatherDataService(WeatherDataServiceFactory.OPEN_WEATHER_MAP);
				Location location = new Location(51.51,-0.13);
				WeatherData weatherData = service.GetWeatherData(location);
				Console.WriteLine(weatherData + "\n");

				IWeatherDataService service2 = WeatherDataServiceFactory.
					GetWeatherDataService(WeatherDataServiceFactory.APIXU);
				WeatherData weatherData2 = service2.GetWeatherData(/*new Location(51.52,-0.11)*/location);
				Console.WriteLine(weatherData2);
			}
			catch(WeatherDataServiceException e)
			{
				Console.WriteLine(e.Message);
			}
			Console.Read();     //Remove it if you are using Window Application output type.
		}
	}
}