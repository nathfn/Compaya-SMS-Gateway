# Compaya-SMS-Gateway

This is a .NET/C# implementation of the Compaya SMS Gateway API (https://api.cpsms.dk/documentation/index.html). The only dependency is Newtonsoft.Json version 9.0.1+

Before you begin you need:
- Download the latest release: https://github.com/nathfn/Compaya-SMS-Gateway/releases/download/v1.0.0.0/CompayaSmsGateway-1.0.0.0.zip
- A username from https://www.cpsms.dk
- An API key which can be obtained after creating the user above

Using the API:
The API is simple. Initialize one of the four repositories to access the API:
- SmsRepository: Used to send SMS messages.
- GroupRepository: Used to manage groups (CRUD)
- ContactRepository: Used to manage contacts in groups (CRUD)
- LogRepository: Used to access log history

When you initialize a repository you need to specify username and API key like this:

var smsRepository = new SmsRepository("username", "api_key");

And then you can send an SMS:

smsRepository.SendSms(new[] { "4512345678" }, "Hello world!" "4587654321");

Almost all interaction with the repositories will return an object which is a subclass of ResponseModel.cs. This model will always return the following:

.HttpStatusCode: The status code (if any) returned from the API call. This can be used to test against. See more about status codes here: https://api.cpsms.dk/documentation/index.html#errors

.Exception: The exception thrown by the code (if any). Can be useful for debugging or logging.
