# WooliesShopping

Pre-Requiste to run API code:
1.	Open the Woolies.ShoppingCart.sln in Visual Studio 2019. It requires .Net Core 3.1 Version
2.	Buildthe application
3.	Run the code.
4.	The API will open the browser with port number http://localhost:59338/shopping/user

Best Practices used in API:
1.	Created separate project for models and interface, so that it can be reused by different application.
2.	Logging manager has been implemented in a way to replace the different log provider with minimal impact to API code
3.	Swagger documentation has been done using swash buckle
4.	Postman collection has been generated and uploed in the folder
5.	Implmented named httpclientfactory pattern to connect the woolworth api's
6.	Implemented parallel tasks concepts to span across different threads to get the results from the search engine.
7.	Async / Await has been implemented not to block the thread.
8.	Request validation has been done using ValidationAttribute.
9.	Logging mechanism has been implemeneted to capture the activities in the application and the exception
10.	Generic exception handler mechanism has been implemented.
11.	Unit test has been written and tested for all important functions.
