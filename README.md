# NetChill Online Movie Streaming Website 

## Software Design Principle - Clean Architecture 

1. Core
	* Domain Layer
	* Application Layer

2. Infrastructure
	* Infrastructure Layer
	* IoC Layer (Inversion of Control)
	* Persistence Layer

3. Presentation
	* Angular 16 (ClientApp)
	* ASP.Net Core 6 WebAPI (API)

4. Shared 


# How it works!

## Angular 16 - ClientApp

__NB: Open ClientApp on VS Code__

### Installation Guid

__On VS Code terminal: Run command -__ "npm install --legacy-peer-deps"

The above command will install all required angular packages.

### Run Application

__To start ClientApp, Run command:__ "ng serve -o"


### URL Navigations

* __Admin__ - http://localhost:4200/#/Dashboard
* __User__ - http://localhost:4200/#/Home



## ASP.Net Core 6 WebAPI 

__NB: Open Visual Studio 2022 or Latest__

* Set startup project to WebAPI (Presentation Layer)
* Restore NugetPackages if needed. Do not update, but restore.   


# Screenshots

## Project Structure
<img alt="project structure" src="./NetChill.WebAPI.UI/ClientApp/src/assets/images/screenshots/Project Structure.png">

## Landing Page
<img alt="landing page" src="./NetChill.WebAPI.UI/ClientApp/src/assets/images/screenshots/Landing Page.png">

## Upcoming Movie
<img alt="upcoming movie" src="./NetChill.WebAPI.UI/ClientApp/src/assets/images/screenshots/Upcoming Movie.png">

## Movie Details
<img alt="movie details" src="./NetChill.WebAPI.UI/ClientApp/src/assets/images/screenshots/Movie Details.png">

## Upload Movie Files
<img alt="upload files" src="./NetChill.WebAPI.UI/ClientApp/src/assets/images/screenshots/Movie files upload.png">