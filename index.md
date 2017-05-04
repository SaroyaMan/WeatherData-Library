### Description
This library made to developers who wants to get easly the current Weather in different places around the world.
This library  made for C# developers. The big advantage that using that library is very easy, the code is an [open source,](https://github.com/SaroyaMan/WeatherData-CSharp) so you can easly create your own WeatherData REStful web services just by creating a class that implements IWeatherDataService, and add the relevant constant to the static WeatherDataServiceFactory class (don't forget to extend the switch-case in GetWeatherDataService inside the factory class).

### How-to install
1. Download and extract the WeatherLibrary.zip from my [git page.](https://github.com/SaroyaMan/WeatherData-CSharp)
2. On a project you are willing to use the library - right click on project and then Add -> Reference.
3. Browse all DLL files from the extracted zip.
4. Add the directive 'Weather' (using Weather;) inside your source file.
5. That's it, you can now use the library!

### API in a nutshell
#### The classes:
* **Location:** Represents the location data of the WeatherData object is based on.
* **WeatherData:** Represents the weather of the selected location. All fields in this class are defined as read-only. once WeatherData is created by a specific location, you cannot change it's state.
* **IWeatherDataService:** Represents a weather REStful web service. each class that implements it must create WeatherData object with his own way.
* **WeatherDataServiceException:** Extends Exception.Represents an exception that caused by using the library as a result of bad connection to one of the web services, or failing to parse the data, and etc.
* **WeatherDataServiceFactory:** Represents the factory who responsible to create a weather REStful web services objects. Each const variable represents one weather web service. This class implements the factory method design pattern.
* **ApixuDataService:** Represents a REStful web service of http://www.apixu.com and it implements the interface IWeatherDataService - means it responsible to create a WeatherData object with all data according to this web service. This class implements the singleton design pattern
* **OpenWeatherMapDataService:** Represents a REStful web service of http://www.openweathermap.org and it implements the interface IWeatherDataService - means it responsible to create a WeatherData object with all data according to this web service. This class implements the singleton design pattern

#### The Unit Test classes:
* **ApixuDataServiceTest:** Represents a test class that checks ApixuDataService works correctly.
* **OpenWeatherMapDataServiceTest:** Represents a test class that checks OpenWeatherMapDataService works correctly.

### Example
[Video with a short application<br>](https://www.youtube.com/watch?v=uhqKjmem8N8)
This short application demonstrates the usage of Weather library.
***
```c#
using System;
using Weather;

namespace Test
{
	internal class WeatherDemo
	{
		public static void Main()
		{
			try
			{
				IWeatherDataService service = WeatherDataServiceFactory.
					GetWeatherDataService(WeatherDataServiceFactory.OPEN_WEATHER_MAP);
				WeatherData weatherData = service.GetWeatherData(new Location("London"));
				Console.WriteLine(weatherData + "\n");

				IWeatherDataService service2 = WeatherDataServiceFactory.
					GetWeatherDataService(WeatherDataServiceFactory.APIXU);
				WeatherData weatherData2 = service2.GetWeatherData(new Location(51.52,-0.11));
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
```
![Output window](http://s18.postimg.org/3mn4in2e1/output.png)

### Authors and Contributors
For any questions and notes - you can always contact with Yoav Saroya (@SaroyaMan) - stankovic100@gmail.com
* **category:** utility library.
* **last update:** 25/09/2016.
* **authors:** Yoav Saroya - 304835887 & Sharon Yosef - 305345118.

