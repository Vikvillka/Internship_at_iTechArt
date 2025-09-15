## Task description

For the first step, let's create a simple Web API application. 

As a data source, use `List<T>`, where T is your base entity.

The requirements for a base entity are:
- it should implement `IEntity` interface showing it has an `Id` field
- it should have at least one child entity

Create these endpoints:
- `..\getAll` to retrieve the full list of created entities
- `..\get\{id}` to retrieve an entity by its unique identifier
- `..\create` to add a new record to the list
- `..\update` to update an existing record
- `..\delete\{id}` to delete a record by its Id

Requirements for API are: 
- Use .NET 8
- Use Controllers-based approach
- API should expose an endpoint for retrieving OpenAPI data, and the Swagger panel should be accessible
- Use `OkObjectResult` as a result type for each endpoint

Create a record during application initialization to ensure that the data source contains at least one record from the beginning.

Use Swagger and Postman to test your endpoints. Add a `postmanCollection.json` file to the folder containing your API service. Update this file every time you change your API specification.

Include screenshots showing usage of the `getAll` endpoint in both Swagger and Postman in the ticket for this task.

## Topics to learn: 

- What is Web API?
- What does REST stand for?
- What is the difference between the Controller-based approach and Minimal API in ASP.NET?
- What are OpenAPI and Swagger?
- What is Postman? How do I test my API endpoint in Postman? How do I create a collection of endpoints in postman? How do I share this collection?
## Useful links

- https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-9.0&tabs=visual-studio - official Microsoft tutorial for Web API. You don't need to create a dbContext yet though.
- https://swagger.io/specification/ - documentation of Swagger and OpenApi
- https://www.postman.com/ - Postman official website
- https://www.youtube.com/watch?v=CLG0ha_a0q8 - YouTube video with a tutorial for Postman
