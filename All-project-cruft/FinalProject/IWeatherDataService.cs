namespace Weather
{
	/// <summary>
	/// Represents a weather REStful web service. each class that implements it must
	/// create WeatherData object with his own way.
	/// </summary>
	public interface IWeatherDataService
	{
		 WeatherData GetWeatherData(Location location);
	}
}