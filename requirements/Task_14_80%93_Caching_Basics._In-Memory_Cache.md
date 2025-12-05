### Description

In this task, you’ll introduce **in-memory caching** into the Gateway service.  
The goal is to reduce unnecessary HTTP and database calls by caching the response of the `/getEntityById` endpoint for a short period.

When the Gateway needs to fetch the main entity by its ID, it should first check the in-memory cache.

- If the value **exists** in cache → return it immediately (no HTTP call to the main API).
- If the value **is not in cache** → call the Main API, store the result in cache (in-memory in gateway), and then return the value.

Just implement caching for one endpoint - the one that returns your main entity record by  its ID.

**Add Memory Cache to Gateway**
- Register `IMemoryCache` in the Gateway's DI container.
- Inject it into the service that handles `getEntityById`.

Inside the Gateway’s service:

- Check if the ID exists in cache (use `"[entityname]:{id}"` as the key).
- If **found**, return cached value and ensure **no HTTP call** to the Entity API service is triggered.
- If **not found**, call the Main API, get the entity, store it in Gateway service memory cache, then return.

**Set Cache Expiration**
- Apply an **absolute expiration** (e.g., 30–60 seconds) using **MemoryCacheEntryOptions**.
- Make the expiration configurable if possible.

**Make It Testable**
- Add logs to confirm whether the cache was hit or a fetch from the Main API occurred.
- Ensure behavior can be verified by re-triggering the same ID multiple times.

### Topics to Learn

- Concept of memory cache
- **IMemoryCache** interface. Configuring memory cache settings
- The difference between **sliding** and **absolute** expiration