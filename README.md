# movies-app
App for cinephiles and movie hobbyists

BUILD INSTRUCTIONS
* Make sure that all Nugget packages are restored when building. They are commonly restored in response to a build command.
If not, please access the NuggetPackeged Manager for the solution and restore from there.
* This project was developed using this technologies:
	- Visual Studio Community 2015 Update 3
	- .Net Framework 4.5.2
	- Xamarin 4.2.0.695
	- Android SDK min 16/target 23	
	
THIRD-PARTY LIBRARIES
* Moq v4.5.22 - Moq is the most popular and friendly mocking framework for .NET
* Newtonsoft.Json 9.0.1 - Json.NET is a popular high-performance JSON framework for .NET
* Autofac 4.2.0 - Autofac is an IoC container for Microsoft .NET. It manages the dependencies between classes.

ARCHTECTURE CONSIDERATIONS
* BACKEND - Here stands all the code that is not related to the UI and UX
	* DOMAIN - Here stands all the code that is related to the project business
	* INFRASTRUCTURE - Here stands all the code that is related to technology specifics that aids the system fo function
		* SERVER - Here stands all the code that is technology specific for the server side of the application
		* MOBILE - Here stands all the code thath is technology specific for the mobile side of the application
	* SERVICES - Here stands all the code that abstract the access of the system´s internal structure and provides and end point for the clients
	
* FRONTEND - Here stands all the code that is related to the UI and UX
	* XAMARIN - Here stands all the code that use Xamarin technology for display the front-end for the users