Using your entities as request/response models has its pros and cons, so let's explore other management strategies. The main disadvantages of this approach are:
- Sometimes it's unnecessary to include all the fields in a request. For example, fields such as `Id`, `CreatedAt`, and `UpdatedAt` are usually set by the backend, so there's no need to include them in the `create` request.
- Exposing the database schema to the public is insecure.

For each endpoint, create a request and response models. To map request models to entities, use Automapper. To map entities to response models, use static map methods. Usually, one approach is used, but let's learn how to use both.

To set up Autommaper, use `Profile` class and `CreateMap` method. Find a way to register all the profiles automatically, without specifying each in your `Program` class. You should cover both main and child entities with the mapping.

## Topics to learn: 

- Different strategies for mapping: Automapper vs. manual/static methods
- Introduction to Automapper
- Dependency Injection and Automapper Registration

## Useful links

- https://docs.automapper.io/en/stable/Configuration.html - Automapper documentation