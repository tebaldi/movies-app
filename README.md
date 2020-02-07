# movies-app
App for cinephiles and movie hobbyists

VISUAL STUDIO SOLUTIONS
* I´ve created two Solution projects just for show the concepts of the server side structure also
	* MOBILE SOLUTION - Here stands the code that is related for the Mobile Solution to function without the Server specifics
	* SERVER SOLUTION - Here stands the code that is related for the Server Solution to function without the Mobile specifics
	
BUILD INSTRUCTIONS
* Make sure that all Nugget packages are restored when building and they are commonly restored in response to a build command.
If not, please access the NuggetPackeged Manager for the solution and restore from there.
Please pay special atention to check if the Xamarin Android has installed the support libraries correctly.

* This project was developed using this technologies:
	- Visual Studio Community 2015 Update 3
	- .Net Framework 4.5.2
	- Xamarin 4.2.0.695
	- Xamarin.Android 7.0.1.2
	- Android SDK min 5.1/target 6.0
	
THIRD PARTY LIBRARIES
* Moq v4.5.22 - Moq is the most popular and friendly mocking framework for .NET
* Newtonsoft.Json 9.0.1 - Json.NET is a popular high-performance JSON framework for .NET
* Autofac 4.2.0 - Autofac is an IoC container for Microsoft .NET. It manages the dependencies between classes.
* Square.Picasso 2.5.2 - A powerful image downloading and caching library for Android

ARCHTECTURE SOLUTIONS CONSIDERATIONS
* BACKEND - Here stands the code that is not related to the UI and UX
	* DOMAIN - Here stands the code that is related to the project business
	* INFRASTRUCTURE - Here stands the code that is related to technology specifics that supports the system
		* SERVER - Here stands the code that is technology specific for the server side of the application
		* MOBILE - Here stands the code that is technology specific for the mobile side of the application
	* SERVICES - Here stands  the code that abstract the access of the system internal structure and provides a end point for the clients
	
* FRONTEND - Here stands the code that is related to the UI and UX
	* XAMARIN - Here stands the code that use Xamarin technology for display the front-end for the users