# Aterrizar-BE
.NET Core 6 API with Domain Driven Design and Clean Architecture

## Setup project locally. Visual studio code
We make use of [Rest Client](https://github.com/Huachao/vscode-restclient) extension to make requests, so we recommend installing the extension.

### For running the project locally
```csharp
dotnet run --project .\Aterrizar.Api\
```

### Doing request to the API
Inside Aterrizar/Requests you can find a comprehensive list of different request you can perform. In order for you to be able to use the requests, you have to first define a local environment for Rest Client.

After you have installed rest client, press ```Ctrl + ,``` and search for rest client. Look for the section Rest-client: Environment variables and click on edit in settings.json
![image](https://user-images.githubusercontent.com/18756969/178116354-1f2bdf27-b426-425e-b507-5714febae911.png)

Then you can add a Local Env and set the host url to the one you are running the project locally
![image](https://user-images.githubusercontent.com/18756969/178116417-f6cc5a67-e0b4-4b85-a369-23b821626069.png)

Finally go into any .http file and you can send a request and check the result of the endpoint call:
![image](https://user-images.githubusercontent.com/18756969/178116501-ff87b52d-7876-4311-8004-57e6e6880d7d.png)
