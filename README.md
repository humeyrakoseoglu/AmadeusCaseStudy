# Case Study: Flight Search Application QA Tests  (QA Engineer)

## Introduction
This repository contains the QA tests conducted for both the frontend and backend of a Flight Search Application. The tests ensure the proper functionality and reliability of the application. Test scenarios were written using RestSharp, Selenium and C#. Selenium is an open source library used to test web pages. REST APIs can be tested with C# using RestSharp and Newtonsoft.Json.

## Backend Tests

### Description
The backend of the application is tested using RestSharp for making API requests and validating responses.
Before running these tests, the following prerequisites must be met on your computer:
-	Visual Studio or your preferred C# IDE must be installed.
-	RestSharp and Newtonsoft.Json NuGet packages should be added to the project.

### Test Scenarios
1. Check Status Code
  - Endpoint: https://flights-api.buraky.workers.dev/
  -	Verify that the HTTP status code is 200 OK.
2. Check Response Content
  -	Endpoint: https://flights-api.buraky.workers.dev/
  -	Validate the structure of the response:
  -	Each flight should have an Id (integer), From (string), To (string), and Date (string).
  -	Response is expected to be an Object[string -> Array[Flight]] with the specified structure.
3. Check Header
  -	Endpoint: https://flights-api.buraky.workers.dev/
  -	Validate that the "Content-Type" header exists and its value is "application/json".

### Test Implementation
The backend tests are implemented using RestSharp and Newtonsoft.Json libraries. Test cases are written in C#.

Prerequisites:
  -	RestSharp library
  -	Newtonsoft.Json library

### How to Run?
1.	Clone the repository.
2.	Open the solution in Visual Studio or any compatible IDE.
3.	.NET Core
4.	Install NUnit Templates for Visual Studio, Nuget package, Restsharp and Newtonsoft.Json library
5.	Run the BackendTests class.


## Frontend  Tests

### Description
The frontend of the application is tested using Selenium with ChromeDriver for UI automation.
Before running these tests, the following prerequisites must be met on your computer
-	Google Chrome browser must be installed.
-	Selenium WebDriver and its relevant dependencies must be added to the project.

### Test Scenarios
1.	Search for Different Cities (Exist Flight Test Case)
  -	Open the Flight App.
  -	Verify that the "From" and "To" input fields do not accept the same value.<br/>
  `FOR EXISTS FLIGHT :`
This test checks that flights between different cities are listed successfully. It queries for a flight from Istanbul to Los Angeles and verifies that two flights are listed.<br/>
  `FOR A FLIGHT NOT FOUND :`
This test checks that a correct message is displayed in the case of no flights. You query a flight from Istanbul to Paris and get "There are no flights between these two cities." Verifies that the message is displayed.

2.	Search for Same City
  -	Open the Flight App.
  -	Verify that the "From" and "To" input fields do not accept the same value, and that what you select in “From” cannot be selected in “To”<br/>
This test checks that flight queries cannot be made between the same cities. Queries for a flight from Istanbul to Istanbul and verifies the absence of options in the "To" field.

3.	List Test
  -	Open the Flight App.
  -	Select different cities in the "From" and "To" input fields.
  -	Ensure that the number of listed flights matches the "Found X items" count.<br/>
This test checks whether the number specified in the "Found X items" text actually matches the number of flights listed. It queries a flight from Istanbul to London and verifies whether these numbers match.

### Test Implementation
The frontend tests are implemented using Selenium with ChromeDriver. Test cases are written in C#.

Prerequisites:
-	ChromeDriver
-	Selenium library for C#

### How to Run?
1.	Clone the repository.
2.	Open the solution in Visual Studio or any compatible IDE.
3.	.NET Core
4.	Install NUnit Templates for Visual Studio
5.	Install Nuget package
6.	Install Selenium and ChromeDriver
7.	Run the FrontendTests class.

## Notes
-	All test code is available in the respective classes in the repository.
-	Ensure that the required libraries and drivers are set up before running the tests.
-	The test results and logs can be found in the console output.

## TEST RESULTS
![image](https://github.com/humeyrakoseoglu/AmadeusCaseStudy/assets/71442681/f0303501-f181-43f1-9dcf-7ed13a873c55)



