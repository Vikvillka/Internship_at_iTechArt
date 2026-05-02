# Cartograph: Repository Mapping Skill

## Goal
Create a complete architectural map of a microservices-based system with a React frontend.
The output should help AI agents quickly understand service boundaries, communication patterns, and internal structure.

---

## Context

This project is a **community-driven web application** built during an internship.

### Architecture:
- Microservices-based backend
- Onion Architecture inside each service:
  - Domain
  - Application
  - Infrastructure
  - API

### Backend services:
- CommunityService (communities & events)
- UserService (users & authentication)
- HistoryService (background processing & audit)
- Gateway (API Gateway)
- AppHost (orchestration)

### Communication:
- REST (Gateway → CommunityService via Refit)
- gRPC (Gateway → UserService)
- RabbitMQ (async events)
- HistoryService consumes events

### Frontend:
- React + TypeScript
- Redux Toolkit + Redux Saga
- Material UI

---

## Instructions

Analyze the repository and produce a structured markdown document.

---

## 1. High-Level Overview

Explain:
- What the system does
- Key architectural decisions
- Why microservices are used here

---

## 2. Solution Structure

Break down ALL major parts:

### Backend
Explain each service:

- AppHost
- Gateway.API
- CommunityHub.*
- UserService.*
- HistoryService.*

For each service:
- Responsibility
- Internal layers (Domain, Application, etc.)
- Database ownership (if visible)

### Frontend
Explain structure of `/frontend/src`:

- api
- components
- containers
- hooks
- store
- pages
- layouts

---

## 3. Onion Architecture Breakdown

For ONE representative service (e.g. CommunityHub), explain:

- Domain layer responsibilities
- Application layer responsibilities
- Infrastructure layer responsibilities
- API layer responsibilities

---

## 4. Key Entry Points

Identify:

### Backend:
- API entry points (Controllers)
- gRPC services
- Worker entry (HistoryService.Worker)
- Gateway routing

### Frontend:
- index.tsx
- App.tsx
- routing (AppRoutes.tsx)

---

## 5. Service Communication

Describe:

- Gateway → CommunityService (REST)
- Gateway → UserService (gRPC)
- RabbitMQ event flow
- HistoryService event handling

---

## 6. Data Flow

Explain typical flows:

Example:
- User registers
- User joins community
- Event is created

Show how requests travel across services.

---

## 7. Shared Contracts

Explain:
- Role of Contracts projects
- NuGet usage
- Why this is important for microservices

---

## 8. External Dependencies

List and explain:

- ASP.NET Core
- Entity Framework Core
- AutoMapper
- FluentValidation
- RabbitMQ
- Refit
- gRPC
- Redux Toolkit
- Redux Saga
- Material UI

---

## 9. Testing

Explain:
- Test projects (e.g. *.Tests)
- Tools used (xUnit, Moq)

---

## 10. Notes for AI Agents

Highlight:

- Each service is independent
- Do NOT mix domain logic across services
- Contracts must stay backward-compatible
- Follow Onion Architecture strictly
- Async communication is critical (RabbitMQ)

---

## Output format

Generate a file:

docs/CODEBASE_MAP.md

With sections:

- Overview
- Architecture
- Services
- Frontend
- Communication
- Data Flow
- Dependencies
- Testing
- Notes