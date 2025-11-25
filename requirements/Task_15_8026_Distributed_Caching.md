### Description

In this task, you’ll extend your caching knowledge beyond in-memory storage and move to **Redis**, which is a distributed cache.  
This is important because in-memory caching only works inside a single instance of the service, while Redis allows all instances to share the same cache.

The flow for caching user data should now look like this:

1. **Gateway → Redis**
    - When the Gateway needs user data (`GetUserById`), it first checks Redis.
    - If the value exists → return it immediately (no HTTP call to the User Service).
2. **Gateway → UserService → Redis**
    - If Redis does NOT have the cached user, the Gateway calls the User Service.
    - The User Service fetches the user from the database.
    - The User Service **stores** the user in Redis for future cache hits.
    - Then it returns the value to the Gateway.
3. **Deletion should invalidate the cache**
    - When a user is deleted, the User Service must remove the corresponding Redis key to ensure stale data isn’t returned.

Run your Redis instance in the separate docker container.
Configure the connection string to Redis for each service that uses it.

**Add Logging**
Log when:
  - The Gateway gets a cache hit
  - The Gateway misses and calls User Service
  - User Service writes to Redis
  - User deletion triggers cache invalidation
### Topics to Learn
- Concept of disrtibuted cache
- What is Redis and how to run it in docker