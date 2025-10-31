### Description

In this task, you’ll build a **Gateway API** that communicates with your existing main API using **HTTP** requests.  
The goal is to learn how to decouple APIs, handle outbound HTTP communication, and use **Refit** for strongly-typed REST clients.
You’ll create a new **Gateway API project** that exposes simplified endpoints, which internally call the corresponding endpoints from your **original API**.

Create a new ASP.NET Core Web API project named `GatewayApi`. Let's create a new solution folder for it and also move existing projects to a separate folder.

The Gateway should not have its own database. It should communicate with the original API via HTTP calls. Base URL for the original API should come from configuration (e.g., `appsettings.json` → `"ApiBaseUrl": "https://localhost:5001"`).

Install **Refit** NuGet package.
Create an interface (e.g., `IMyBestApi`) representing endpoints from the original API.
Register Refit clients in the Gateway `Program.cs` using:
`builder.Services.AddRefitClient<IMyBestApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["MyBestApiBaseUrl"]));`

This refit client will need you request/response models. To not duplicate the classes, I suggest you to create a separate csproj for the models and expose them to your Gateway project. It would be even better, if you don't link them directly. Try to pack it to a nuget package and install it in your gateway project. Make sure you support versioning for that package - if your models are updated, or some are added, it's better to just increment version of the package, instead of overwriting it. You can add `<Version>1.0.0</Version>` flag in you csproj file, so that when you create a package version, it would be `1.0.0`. When the next version should be create, update the flag value to `1.0.1` and so on.

Duplicate all the endpoints you have right now, so that you can call them from your gateway, and gateway returns the result from your original API service.

For your gateway, use the same authentication you had before (jwt for your main entities, basic for the user deletion, public for the user creation). Meanwhile, change the authentication in your original api to be Basic for each endpoint. Gateway should authorize in your original api with username and password then. Specify them in the configuration, as well as `BaseUrl`. 
### Topics to Learn
- Understanding API gateway pattern basics
- Using **Refit** for strongly-typed HTTP clients
- Authorization configuration for **Refit Client**
- Managing configuration for external service URLs and credentials
- Working with async HTTP calls and error handling
- Handling request/response mapping between gateway and target API
- Clean separation between gateway and backend API layers.
### Useful links
- https://www.milanjovanovic.tech/blog/refit-in-dotnet-building-robust-api-clients-in-csharp - how to use Refit 
- https://microservices.io/patterns/apigateway.html - about gateway api