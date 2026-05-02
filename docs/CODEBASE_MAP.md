# CODEBASE MAP

> Generated via Cartograph. Intended as a reference for AI agents and developers navigating this repository.

---

## 1. Overview

**CommunityHub** is a community-driven web application where users can create communities, host events, subscribe to communities, and participate in events. It is built as a microservices system with a React/TypeScript frontend.

**Key architectural decisions:**

- **Microservices** — each domain (communities/events, users/auth, history) is a separate deployable service with its own database, ensuring independent scaling and deployment.
- **Onion Architecture** — every backend service is layered as Domain → Application → Infrastructure → API, keeping domain logic free of framework dependencies.
- **Gateway pattern** — the single public entry point for the frontend is `Gateway.API`, which fans out to downstream services via REST (Refit) and gRPC.
- **Async communication** — events published to RabbitMQ decouple CommunityHub from HistoryService, avoiding tight coupling for audit/history concerns.
- **.NET Aspire** — used for local orchestration, service discovery, and distributed tracing across all services.

---

## 2. Architecture Diagram

```
┌─────────────────────────────────────────────────┐
│                React Frontend                    │
│        (Redux Toolkit + Redux Saga + MUI)        │
└──────────────────────┬──────────────────────────┘
                       │ HTTP (REST)
                       ▼
              ┌─────────────────┐
              │  Gateway.API    │  ← single public entry point
              └──────┬─────────┘
                     │
          ┌──────────┴────────────┐
          │ REST (Refit)          │ gRPC
          ▼                       ▼
 ┌─────────────────┐    ┌──────────────────┐
 │ CommunityHub    │    │  UserService     │
 │     .API        │    │  .GRpc + .API    │
 └────────┬────────┘    └──────────────────┘
          │ RabbitMQ publish
          ▼
 ┌─────────────────────┐
 │  HistoryService     │
 │     .Worker         │  ← consumes events, writes audit log
 └─────────────────────┘
```

---

## 3. Solution Structure

### 3.1 Backend Services

All backend projects live inside `CommunityHub/`.

#### AppHost (`AppHost/AppHost.AppHost`, `AppHost/AppHost.ServiceDefaults`)

- **Responsibility**: Orchestrates all services using .NET Aspire. Provides shared service defaults: OpenTelemetry distributed tracing, health checks, service discovery.
- **Layers**: No domain logic — infrastructure/orchestration only.

---

#### Gateway.API

- **Responsibility**: Single public API gateway. Authenticates requests, routes to CommunityHub.API via Refit HTTP client, routes to UserService via gRPC clients, handles caching.
- **Controllers**: `AuthGatewayController`, `UserGatewayController`, `CommunityGatewayController`, `EventGatewayController`, `TagGatewayController`, `ImageGatewayController`, `SubscriptionGatewayController`, `ParticipationController`
- **Refit client**: `Clients/IBestApiClient.cs` — typed HTTP interface to CommunityHub.API
- **gRPC clients**: `AuthGrpcClient`, `UserGrpcClient`, `SubscriptionGrpcClient`, `ParticipationGrpcClient`
- **Caching**: `MemoryCacheApiService` (in-memory) and `RedisCacheApiService` (distributed)
- **Auth**: `BasicAuthenticationHandler`, `BasicAuthMessageHandler` (forwards Basic credentials downstream)
- **Database ownership**: None — stateless proxy.

---

#### CommunityHub.* (Community & Events service)

The core domain service for communities and events.

| Project | Responsibility |
|---|---|
| `CommunityHub.Domain` | Entities, enums, interfaces — pure business model |
| `CommunityHub.Application` | Use-case services, repository interfaces — orchestrates domain |
| `CommunityHub.Infrastructure` | EF Core repositories, DbContext, RabbitMQ publisher, image service |
| `CommunityHub.API` | HTTP controllers, DI wiring, Serilog, Swagger, middleware |
| `CommunityHub.Contracts` | Shared DTOs consumed by Gateway.API and tests |

**Entities**: `Community`, `Event`, `EventTag`, `BaseEntity`

**Controllers**: `CommunityController`, `EventController`, `ImageController`, `TagController`

**Database**: PostgreSQL via EF Core (`CommunityHubDbContext`). Migrations run on startup.

**RabbitMQ**: `RabbitMqPublisher` publishes domain events (e.g., community/event created/deleted) consumed by HistoryService.

---

#### UserService.* (Users & Authentication)

Manages users, authentication (JWT), community subscriptions, and event participations.

| Project | Responsibility |
|---|---|
| `UserService.Domain` | `User`, `CommunitySubscription`, `EventParticipation` entities |
| `UserService.Application` | Services for users, subscriptions, participations |
| `UserService.Infrastructure` | EF Core repos, `JwtService`, Redis cache, `RabbitMqListener` |
| `UserService.API` | REST controllers (`AuthController`, `UserController`, `CommunitySubscriptionController`, `EventParticipationController`) |
| `UserService.GRpc` | gRPC server exposing `AuthService`, `UserService`, `SubscriptionService`, `ParticipationService` |
| `UserService.Contracts` | Shared DTOs for auth, users, subscriptions, participations |

**Database**: Own PostgreSQL database (`UserServiceDbContext`), isolated from CommunityHub.

**gRPC protos** (`UserService.GRpc/Protos/`): `auth.proto`, `User.proto`, `Subscription.proto`, `Participation.proto`, `common.proto`

**RabbitMQ**: `RabbitMqListener` in Infrastructure consumes events published by CommunityHub (e.g., to handle cascading deletes or sync state).

---

#### HistoryService.* (Audit / Background Processing)

Asynchronous consumer for audit log entries.

| Project | Responsibility |
|---|---|
| `HistoryService.Domain` | `HistoryRecord` entity |
| `HistoryService.Application` | RabbitMQ consumer logic |
| `HistoryService.Infrastructure` | EF Core data, repositories, migrations |
| `HistoryService.Contracts` | `HistoryRecordDTOs`, `DeleteEntityDTOs` |
| `HistoryService.Worker` | `Program.cs` — hosted service (Worker), registers consumers on startup |

**Database**: Own PostgreSQL database for audit records.

**Entry point**: `HistoryService.Worker/Program.cs` — runs as a background worker, no HTTP surface.

---

### 3.2 Frontend (`frontend/src/`)

React + TypeScript SPA.

| Directory | Responsibility |
|---|---|
| `api/` | Axios API call functions per resource: `community.ts`, `event.ts`, `user.ts`, `participation.ts`, `subscription.ts` |
| `models/` | TypeScript interfaces: `Community`, `Event`, `User`, `Tag`, `Subscription`, `Participation`, `Common` |
| `store/features/` | Redux slices + sagas per domain: `communities/`, `events/`, `users/`, `loader/` |
| `store/` | `rootReducer.ts`, `rootSaga.ts`, `index.ts` — Redux store wiring |
| `pages/` | Page-level components: `homePage/`, `communityDetailsPage/`, `eventDetailsPage/` |
| `components/` | Pure presentational components: `common/`, `community/`, `event/` |
| `containers/` | Connected components (dispatch + selectors): `common/`, `community/`, `event/` |
| `layouts/` | `AppLayout.tsx`, `homePageLayout/` — page shell/chrome |
| `hooks/` | `useBreakpoints.ts`, `useHomePageData.ts` — reusable React hooks |
| `constants/`, `helpers/`, `styles/` | Shared utilities and global styles |
| `AppRoutes.tsx` | React Router route definitions |
| `App.tsx` | Root component — wraps layout and routes |

---

## 4. Onion Architecture Breakdown (CommunityHub as reference)

### Domain Layer (`CommunityHub.Domain`)

- Contains entities (`Community`, `Event`, `EventTag`), enums (`EventStatus`), and repository interfaces (`ICommunityRepository`, `IEventRepository`, `ITagRepository`).
- No dependencies on any framework or infrastructure.
- `BaseEntity` provides common `Id` field. `PagedResult<T>` is a domain value object for pagination.

### Application Layer (`CommunityHub.Application`)

- Contains `CommunityService`, `EventService`, `TagService` — orchestrate repository calls and business rules.
- Depends only on Domain interfaces — never on EF Core or HTTP.
- Defines `IRepository<T>` (generic) and specialized repository interfaces consumed by Infrastructure.

### Infrastructure Layer (`CommunityHub.Infrastructure`)

- Implements repository interfaces using EF Core: `EfRepository<T>`, `CommunityRepository`, `EventRepository`, `TagRepository`.
- Contains `CommunityHubDbContext` with entity configurations.
- Contains `RabbitMqPublisher` (messaging) and `ImageService` (file storage).
- Depends on Application interfaces — satisfies them by providing concrete implementations.

### API Layer (`CommunityHub.API`)

- ASP.NET Core host: DI registration, middleware pipeline, controller routing.
- `Program.cs` wires all layers together, runs EF migrations, configures Serilog, Swagger, and Aspire service defaults.
- `BasicAuthenticationHandler` validates credentials.
- FluentValidation validators live in `Validators/`.
- Exception handling in `ExceptionHandlers/` and custom middleware in `Middlewares/`.

---

## 5. Key Entry Points

### Backend

| Entry Point | Location |
|---|---|
| CommunityHub REST API | `CommunityHub.API/Program.cs` |
| UserService REST API | `UserService.API/Program.cs` |
| UserService gRPC server | `UserService.GRpc/` — `AuthServiceImpl`, `UserServiceImpl`, `SubscriptionServiceImpl`, `ParticipationServiceImpl` |
| Gateway (public API) | `Gateway.API/Program.cs` |
| HistoryService worker | `HistoryService.Worker/Program.cs` |
| Aspire orchestration | `AppHost/AppHost.AppHost/Program.cs` |

### Frontend

| Entry Point | Location |
|---|---|
| App root | `frontend/src/App.tsx` |
| Route definitions | `frontend/src/AppRoutes.tsx` |
| Redux store | `frontend/src/store/index.ts` |
| Root saga | `frontend/src/store/rootSaga.ts` |

---

## 6. Service Communication

### Gateway → CommunityHub (REST via Refit)

`Gateway.API` uses the Refit library to call `CommunityHub.API` over HTTP. `IBestApiClient` is a typed C# interface; Refit generates the HTTP client implementation at runtime. This keeps Gateway code strongly-typed against the CommunityHub contract.

### Gateway → UserService (gRPC)

`Gateway.API` uses generated gRPC stubs (from `auth.proto`, `User.proto`, etc.) via `AuthGrpcClient`, `UserGrpcClient`, etc. gRPC provides efficient binary serialization and strong typing for internal service calls.

### CommunityHub → RabbitMQ → HistoryService

When CommunityHub performs domain operations (create/update/delete community or event), `RabbitMqPublisher` publishes a message to RabbitMQ. `HistoryService.Worker` has a consumer that receives these messages and writes `HistoryRecord` entries to its own database.

### CommunityHub → RabbitMQ → UserService

`RabbitMqListener` in `UserService.Infrastructure` also consumes certain RabbitMQ events (e.g., community deleted) to keep subscription data consistent without synchronous coupling.

---

## 7. Data Flow Examples

### User Registration

```
Frontend → POST /api/auth/register (Gateway)
  → Gateway: UserGrpcClient.RegisterUser()
    → UserService.GRpc: UserServiceImpl.RegisterUser()
      → UserService.Application: UserService.CreateUser()
        → UserService.Infrastructure: UserRepository.Add()
          → PostgreSQL (UserService DB)
```

### User Joins Community

```
Frontend → POST /api/subscriptions (Gateway)
  → Gateway: SubscriptionGrpcClient.CreateSubscription()
    → UserService.GRpc: SubscriptionServiceImpl.CreateSubscription()
      → UserService.Application: CommunitySubscriptionService.CreateSubscription()
        → PostgreSQL (UserService DB)
```

### Event Created (with audit trail)

```
Frontend → POST /api/events (Gateway)
  → Gateway: IBestApiClient.CreateEvent() [Refit → HTTP]
    → CommunityHub.API: EventController.CreateEvent()
      → CommunityHub.Application: EventService.CreateEvent()
        → CommunityHub.Infrastructure: EventRepository.Add() → PostgreSQL
        → RabbitMqPublisher.Publish("event.created")
          → RabbitMQ broker
            → HistoryService.Worker consumer
              → HistoryService DB: HistoryRecord persisted
```

---

## 8. Shared Contracts

Each service has a `*.Contracts` project containing DTOs (request/response models):

- `CommunityHub.Contracts` — community/event DTOs consumed by Gateway.API
- `UserService.Contracts` — auth/user/subscription/participation DTOs
- `HistoryService.Contracts` — history record DTOs

**Why this matters for microservices**: Contracts are the API surface boundary between services. They must remain backward-compatible — adding fields is safe, removing or renaming is a breaking change. In a CI/CD pipeline these would typically be published as NuGet packages so consumer services can take explicit version dependencies.

---

## 9. External Dependencies

### Backend

| Dependency | Purpose |
|---|---|
| **ASP.NET Core** | Web framework for all HTTP services |
| **Entity Framework Core** | ORM for PostgreSQL access in CommunityHub, UserService, HistoryService |
| **AutoMapper** | Maps between domain entities and DTOs |
| **FluentValidation** | Request validation with rules decoupled from controllers |
| **Serilog** | Structured logging with sinks (console, file, etc.) |
| **RabbitMQ** | Async message broker for event publishing/consumption |
| **Refit** | Strongly-typed REST client used in Gateway → CommunityHub calls |
| **gRPC / Protobuf** | Efficient binary RPC for Gateway → UserService calls |
| **.NET Aspire** | Local orchestration, distributed tracing, health checks |
| **Redis** | Distributed cache in Gateway.API (`RedisCacheApiService`) |

### Frontend

| Dependency | Purpose |
|---|---|
| **React** | Component-based UI framework |
| **TypeScript** | Static typing for safety and IDE support |
| **Redux Toolkit** | State management with slices and selectors |
| **Redux Saga** | Side-effect management for async API calls |
| **Material UI (MUI)** | Component library for consistent UI |
| **Axios** | HTTP client for API calls to Gateway |

---

## 10. Testing

### Backend test projects

| Project | Scope |
|---|---|
| `CommunityHub.Tests` | Unit tests for CommunityHub.Application services |
| `UserService.Tests` | Unit tests for UserService.Application services |

**Tools used:**

- **xUnit** — test runner and assertion framework
- **Moq** — mocking library for isolating dependencies (repositories, external services)

Tests follow the Arrange-Act-Assert pattern. Services are tested in isolation with mocked repository interfaces, keeping tests fast and deterministic.

---

## 11. Notes for AI Agents

- **Service independence**: Each service (CommunityHub, UserService, HistoryService) owns its data and must not share a database or directly call another service's repository.
- **Do NOT mix domain logic across services**: Community business rules live only in `CommunityHub.Domain/Application`. User business rules live only in `UserService.Domain/Application`.
- **Contracts must stay backward-compatible**: Adding fields to DTOs is safe. Removing or renaming fields is a breaking change affecting all consumers.
- **Onion Architecture is strict**: Dependencies flow inward only — Infrastructure depends on Application, Application depends on Domain. The API layer depends on Application. Never bypass this.
- **Async communication is critical**: Use RabbitMQ for cross-service side effects. Never add synchronous HTTP calls between CommunityHub and HistoryService — this would couple them.
- **Gateway is the only public entry point**: The frontend calls Gateway exclusively. CommunityHub.API and UserService.API are internal and not exposed directly in production.
- **gRPC protos are the contract for UserService**: Changes to `.proto` files in `UserService.GRpc/Protos/` are breaking changes that require updating Gateway.API's generated stubs.
