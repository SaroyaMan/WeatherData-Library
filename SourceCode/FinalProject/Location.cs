namespace Weather
{
	/// <summary>
	/// Represents the location data of the WeatherData object is based on.
	/// This class implements the prototype design pattern by implements the ICloneable interface.
	/// </summary>
	public class Location
	{
		public string City { get; private set; }
		public double Longitude { get; private set; }
		public double Latitude { get; private set; }
		public string Country { get; private set; }

		/// <summary>
		/// IsInitialized checks if location is already defined.
		/// It made to make sure that each Location object belongs to exactly 1 WeatherData object
		/// </summary>
		public bool IsInitialized { get; private set; }

		/// <summary>
		/// A humble constructor just to make sure we create the location object with a city searched string
		/// </summary>
		/// <param name="searchCityTxt"><see cref="City"/></param>
		public Location(string searchCityTxt)
		{
			City = searchCityTxt;
			Country = "SomeCountry";		//just to ensure printing the location looks fine
			Longitude = Latitude = -1;
		}

		/// <summary>
		/// A humble constructor just to make sure we create the location object with a latitude and longitude searched string
		/// </summary>
		/// <param name="searchLatitude"><see cref="Latitude"/></param>
		/// <param name="searchLongitude"><see cref="Longitude"/></param>
		public Location(double searchLatitude, double searchLongitude)
		{
			Latitude = searchLatitude;
			Longitude = searchLongitude;
			City = "SomeCity";          //just to ensure printing the location looks fine
			Country = "SomeCountry";    //just to ensure printing the location looks fine
		}

		/// <summary>Initializing all the properties of the location.</summary>
		/// <param name="city"><see cref="City"/></param>
		/// <param name="longitude"><see cref="Longitude"/></param>
		/// <param name="latitude"><see cref="Latitude"/></param>
		/// <param name="country"><see cref="Country"/></param>
		public void Initialize(string city,double longitude,double latitude,string country)
		{
			if (!IsInitialized)
			{
				City = city;
				Longitude = longitude;
				Latitude = latitude;
				Country = country;
				IsInitialized = true;	//making sure initialize function only occurs 1 time per object
			}
		}

		public override string ToString()
		{
			return $"{City} in {Country} with latitude {Latitude}, longitude {Longitude}.";
		}
	}
}