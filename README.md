# Internship at iTechArt
## Project idea

This project was developed as part of an internship assignment and represents a community-driven web application designed to bring people together based on shared interests and events.

## Technologies & Architecture

### Architectural Approach

* **Microservices-based architecture**: the solution is split into multiple independent services
* **Onion Architecture** applied inside each service:
  * Domain layer
  * Application layer
  * Infrastructure layer
  * API layer
* **Shared Contracts project** for DTO definitions
  * Packaged and reused via **NuGet**
  * Ensures consistent data contracts between services

### Solution Structure (Backend)

The backend consists of several independent projects:

* **CommunityService** – main service for managing communities and events
* **UserService** – user management and authentication service
* **Gateway** – API Gateway that routes requests to internal services
* **HistoryService (Worker)** – background service for audit/history tracking
* **Aspire Orchestrator** – service orchestration and infrastructure management

### Service Communication

* **Gateway → CommunityService**: REST API using **Refit client**
* **Gateway → UserService**: **gRPC** communication
* **Asynchronous communication** between services using **RabbitMQ**
* **HistoryService Worker** listens to domain events via RabbitMQ and persists changes into its own database

### Backend Technologies

* **ASP.NET Core (.NET 8)**
* **Entity Framework Core**
* **AutoMapper**
* **FluentValidation**
* **Global Exception Handling**
* **Structured Logging**
* **Authentication & Authorization**:
  * Basic Authentication (service-to-service)
  * JWT Authentication (client-facing)
* **API Gateway Pattern**
* **REST & gRPC**
* **xUnit & Moq**

---
### Frontend Technologies

* **React**
* **TypeScript**
* **React Router DOM**
  * Routing
  * Details pages
* **Redux Toolkit**
* **Redux Saga** for side effects and async flows
* **Material UI (MUI)** for UI components
---

## Internship Requirements

The **requirements** folder contains the original internship task descriptions and conditions that were implemented as part of this project.

---
## NuGet Packages
This project demonstrates creating and using a shared Contracts NuGet package for DTO exchange between microservices.
To create your own NuGet package you need:
### 1. Configure project file for package generation
Add the following properties to your `.csproj` file:
```xml
<PropertyGroup>
  <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
  <PackageId>CommunityHub.Contract.Package</PackageId>
  <Version>1.0.0</Version>
  <PackageOutputPath>../../packages</PackageOutputPath>
</PropertyGroup>
```
### 2. Adding local NuGet source for your project
```bash
dotnet nuget add source "<path_to_packeges_folder>" -n "<NameLocalSource>"
```
### 3. Reference the package in your project
```xml
<PackageReference Include="CommunityHub.Contract.Package" Version="1.0.0" />
```
To support versioning, change the version in the `.csproj` file to another one:
```xml
<Version>1.0.1</Version>
```
And build the project
---
## Environment Variables and Docker Compose
The project uses a `.env` file to store environment-specific variables for Docker Compose. This file contains sensitive data like database credentials, JWT secrets, and API configuration. The repository contains a sample `.env.example` file. You should copy it to `.env` and replace placeholders with your real values.
