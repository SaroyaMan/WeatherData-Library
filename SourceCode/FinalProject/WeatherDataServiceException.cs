using System;

namespace Weather
{
	/// <summary>
	/// Represents an exception that caused by using the library as a result of bad connection
	/// to one of the web services, or failing to parse the data, and etc.
	/// </summary>
	public class WeatherDataServiceException: Exception
	{
		public WeatherDataServiceException() {}
		public WeatherDataServiceException(string msg) : base(msg) {}
		public WeatherDataServiceException(string msg, Exception inner) : base(msg, inner) {}
	}
}