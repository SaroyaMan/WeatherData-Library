namespace Weather
{
	/// <summary>
	/// Represents the weather of the selected location.
	///	All fields in this class are defined as read. once WeatherData is created by a specific location,
	/// you cannot change it's state.
	/// </summary>
	public class WeatherData
	{
		/// <summary>
		/// Represents the location of the WeatherData is set by it.
		/// once location is assigned - you cannot change it (location.IsInitialized helps to protect it)
		/// </summary>
		public readonly Location Location; //represents the location of this Weather data

		/// <summary>
		/// Represents the Wind Data of the weather.
		/// </summary>
		public readonly Wind WindInfo;

		public readonly double Temperature;
		public readonly double Humidity;
		public readonly double Pressure;
		public readonly string Clouds;
		public readonly string LastUpdated;

		/// <summary>Record Constructor</summary>
		/// <param name="location"><see cref="Location"/></param>
		/// <param name="temperature"><see cref="Temperature"/></param>
		/// <param name="humidity"><see cref="Humidity"/></param>
		/// <param name="pressure"><see cref="Pressure"/></param>
		/// <param name="clouds"><see cref="Clouds"/></param>
		/// <param name="lastUpdated"><see cref="LastUpdated"/></param>
		/// <param name="windSpeed"><see cref="WindInfo"/></param>
		/// <param name="windDescription"><see cref="WindInfo"/></param>
		/// <param name="windDirection"><see cref="WindInfo"/></param>
		public WeatherData(Location location, double temperature,double humidity,double pressure,
			string clouds, string lastUpdated, double windSpeed,string windDescription,string windDirection)
		{
			Location = location;
			Temperature = temperature;
			Humidity = humidity;
			Pressure = pressure;
			Clouds = clouds;
			LastUpdated = lastUpdated.Replace('T',' ');
			WindInfo = new Wind(windSpeed,windDescription,windDirection);
		}

		public override string ToString()
		{
			return $"Location: {Location}\ntemperature is {Temperature}°C, humidity is {Humidity}% " +
			       $"with pressure of {Pressure}hPa.\nThe sky is {Clouds}.\n{WindInfo}\nLast update: {LastUpdated}.";
		}

		/// <summary>Represents the Wind of the WeatherData</summary>
		public class Wind
		{
			public readonly double Speed;
			public readonly string Description;
			public readonly string Direction;

			/// <summary>Record Constructor</summary>
			/// <param name="speed"><see cref="Speed"/></param>
			/// <param name="description"><see cref="Description"/></param>
			/// <param name="direction"><see cref="Direction"/></param>
			public Wind(double speed,string description,string direction)
			{
				Speed = speed;
				Description = description;
				Direction = direction;
			}
			
			public override string ToString()
			{
				return $"The wind speed is {Speed}kph, {Description}, to {Direction}.";
			}
		}
	}
}