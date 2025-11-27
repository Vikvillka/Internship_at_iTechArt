### Description

In this task, you’ll containerize your **(Entity) API** and **Gateway API**, along with the **database**, using **Docker**. 
The goal is to learn the basics of Docker — writing Dockerfiles, building images, linking services, and exposing ports for communication between containers.
When finished, you should be able to run the entire system (database + both APIs) using Docker and access endpoints from your local machine.

Learn what is Docker, Docker Container and Docker Compose.

#### **1. Containerize the Database**
- Create a container using the official PostgreSQL image.
- Expose the default PostgreSQL port (`5432`).
- Set credentials and database name via environment variables: `POSTGRES_USER=appuser POSTGRES_PASSWORD=apppassword POSTGRES_DB=appdb.
- Make sure you can call the database the same way you did before - for example, run your EntityApi with the same db connection string.

#### **2. Containerize the (Entity) API**

- Create a `Dockerfile` inside your (Entity) API project.
- Configure environment variables for DB connection string (pointing to the PostgreSQL container)
- Expose the API port (e.g., `5001`).

#### **3. Containerize the Gateway API**

- Add a `Dockerfile` in the gateway project as well.
- Expose port (e.g. `5002`).
- Configure its `ApiBaseUrl` (in `appsettings.json` or environment) to point to the **Main API container**.

#### **4. Use Docker Compose**
Create a `docker-compose.yml` file at the solution root to orchestrate:
    - PostgreSQL
    - Main API
    - Gateway API
Configure inter-container networking via Docker Compose service names.  
Example:
```
services:   
	db:     
		image: postgres     
		ports:       
			- "5432:5432"   
	mainapi:     
		build: ./MainApi     
		ports:       
			- "5001:80"     
		depends_on:       
			- db   
	gateway:     
		build: ./GatewayApi     
		ports:       
			- "5002:80"     
		depends_on:       
			- mainapi
```
### Topics to Learn

- Docker fundamentals: images, containers, layers, ports
- Writing and optimizing multi-stage Dockerfiles for .NET apps
- Docker networking and service discovery
- Environment variable configuration
- Using Docker Compose to manage multi-container applications  
### Useful links
- https://www.docker.com/ - official docker site
- https://www.youtube.com/watch?v=gAkwW2tuIqE&t=466s - what is docker, first steps
- https://www.youtube.com/watch?v=_wp2zJHs9l0 - containerizing dotnet api
- https://www.youtube.com/watch?v=sXjkAEqFZEI - about docker compose
  