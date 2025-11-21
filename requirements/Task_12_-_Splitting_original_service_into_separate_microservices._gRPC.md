### Description

Now, it's time to completely move to microservices architecture.

Previously, we implemented Gateway API and a service that communicates with a database. Usually, there are multiple microservices, and each of them has it's own database. Services are usually managing one entity and some child entities around it. If you see two core entities that are not really related to each other, sense-wise, it means, they should be separated into several microservice. Good example - your main entity and user entity. Yes, they have some connections, you link one to another to know what records are owned by an exact user, but logically - they can be separated. While, for example, UserAddress and User are usually managed together (in case you don't have a complex logic around UserAddress, like in taxi application or application for managing rentals).

Note: user-to-your-main-entity relation will not be managed by the database tools - these entities are now in different databases. You will need to find a way to join them in Gateway. That's one of the differences between the microservice architecture and the monolith.

So, the steps are:
-  think how to split your entities.
-  create a separate solution folder for each service
-  move all the files related to the particular entities to this new services, one by one. Create a dbContext, migrations and other necessary files for that new service
-  don't forget to move all validators, mappers and exception handlers to your new services as well.
-  in your gateway, for the requests, that previously used different entities to process, now call all the necessary services to get the data. Example: I had a request GetRecordsByUser. Records and users have relation many-to-many. I sent user id and got all the records with this user id. Now, I need to call user service to get the records ids, then send a request to the record service and get this records by the list of ids.
-  keep **basic** authentication between any internal service and Gateway, same as you used to access existing internal api service.
-  to access internal models in Gateway project, create a separate nuget package for each microservice.

For the UserService, let's use **gRPC** as a way to call it from your Gateway. Configure gRPC client for your Gateway and set up UserService to support gRPC calls. For ProblemDetails, use `oneof` feature of gRPC - models should be handled correctly even if UserService responded with an error.

For other internal services, use Refit and HTTP calls the same way you've already implemented for the initial api service. 

Optional - if you configured Aspire, add all the services in your Aspire configuration and make sure you set up gRPC logging.

### Topics to Learn
- Concepts of monolith vs microservice architecture
- Service boundaries and domain-driven design basics (bounded contexts)
- Database per service pattern and data ownership
- Handling cross-service data relations
- What is gRPC and how it works
- Setting up gRPC server and client in dotnet
- Creating `.proto` contracts
- Using `oneof` for different responses
- Aggregating data from multiple services
- Implementing basic internal authentication using gRPC