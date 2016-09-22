namespace Weather
{
	/// <summary>
	/// Represents the factory who responsible to create a weather REStful web services objects.
	/// Each const variable represents one weather web service.
	/// This class implements the factory method design pattern.
	/// </summary>
	public static class WeatherDataServiceFactory
	{
		/// <summary>
		/// Represents the weather REStful web service http://www.openweathermap.org/
		/// </summary>
		public const int OPEN_WEATHER_MAP = 1;

		/// <summary>
		/// Represents the weather REStful web service http://www.apixu.com/
		/// </summary>
		public const int APIXU = 2;

		//To add more weather web services add more consts here and expand the switch case.

		/// <summary>
		/// Checks what weather REStful web service the user sent and returns the requested service.
		/// </summary>
		/// <param name="weatherDataService"></param>
		/// <returns>An object represents a weather REStful web service</returns>
		public static IWeatherDataService GetWeatherDataService(int weatherDataService)
		{
			switch(weatherDataService)
			{
				case 1:
					return OpenWeatherMapDataService.GetInstance();
			
				case 2:
					return ApixuDataService.GetInstance();
			/*
				case 3:
					return another WeatherService.GetInstance();
			*/
				default:
					throw new WeatherDataServiceException("You should send one of the constants that" +
					                                      " defined in WeatherDataServiceFactory");
			}
		}
	}
}