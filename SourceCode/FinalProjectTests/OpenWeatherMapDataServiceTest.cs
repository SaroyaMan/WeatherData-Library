using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Weather.DataServiceTests
{
	/// <summary>
	/// Represents a test class that checks OpenWeatherMapDataService works correctly.
	/// </summary>
	[TestClass()]
	public class OpenWeatherMapDataServiceTest
	{
		/// <summary>
		/// Represents a test method that checks that OpenWeatherMap
		/// service actually creates a WeatherData class with all correct data.
		/// if an exception is thrown - the test is failed.
		/// </summary>
		[TestMethod()]
		public void GetWeatherDataTest()
		{
			try
			{
				Location location = new Location("London");
				OpenWeatherMapDataService wd = OpenWeatherMapDataService.GetInstance();
				WeatherData actual = wd.GetWeatherData(location);
				Assert.AreEqual("London",actual.Location.City);
			}
			catch(WeatherDataServiceException)
			{
				Assert.Fail();	//if an exception is caught, the test is fail.
			}
		}
	}
}