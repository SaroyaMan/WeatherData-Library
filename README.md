# WeatherData-CSharp
[Github page<br>](https://saroyaman.github.io/WeatherData-Library/)
[Video with a short application<br>](https://www.youtube.com/watch?v=uhqKjmem8N8)
This project is a library that gives you a Weather data from REStful web services.

This library made to developers who wants to get easly the current Weather in different places around the world. This library made for C# developers. The big advantage that using that library is very easy, the code is an open source, so you can easly create your own WeatherData REStful web services just by creating a class that implements IWeatherDataService, and add the relevant constant to the static WeatherDataServiceFactory class (don't forget to extend the switch-case in GetWeatherDataService inside the factory class).

### How-to install
1. Download and extract the WeatherLibrary.zip from here.
2. On a project you are willing to use the library - right click on project and then Add -> Reference.
3. Browse all DLL files from the extracted zip.
4. Add the directive 'Weather' (using Weather;) inside your source file.
5. That's it, you can now use the library!

### Authors and Contributors
For any questions and notes - you can always contact with Yoav Saroya (@SaroyaMan)
* **category:** utility library.
* **authors:** Yoav Saroya
