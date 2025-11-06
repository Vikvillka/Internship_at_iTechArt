### Description

In this task, you’ll migrate your multi-service setup (Main API, Gateway API, and Database) to use the **.NET Aspire framework** for orchestration, observability, and distributed tracing.  
The goal is to understand how Aspire simplifies multi-project orchestration, dependency wiring, and cross-service observability with minimal configuration.

You’ll replace your existing Docker Compose setup with Aspire orchestration and ensure that the Aspire **Dashboard** provides unified logs and traces across all services — including API requests flowing from the **Gateway**, through the **Main API**, and down to the **Database**.
#### **1. Setup Aspire Orchestration**
- Add an **Aspire AppHost** project to your solution (e.g., `AppHost`). Let's create a separate solution folder for that.
- Add your existing projects (Main API, Gateway API, and Database container) as **distributed applications** in Aspire. 
- Configure proper ports and dependencies for Gateway, Main API, and Database.
- Specify database **Connection String** and **Api Url** as an environment variables
#### **2. Enable Aspire Dashboard**
- Enable and launch the **Aspire Dashboard** for local observability.
- Verify that all services (Gateway, Main API, Database) appear on the dashboard.
- Explore its features:
    - Logs view
    - Traces
    - Resource relationships
    - Service health
#### **3. Distributed Logging & Tracing**
- Ensure **distributed tracing** is enabled so a single request from Gateway → Main API → Database appears in one trace.
- Use `OpenTelemetry` integration provided by Aspire to automatically capture:
    - HTTP requests
    - SQL queries
    - Exceptions
- Verify all logs and traces are visible in the dashboard under a single correlation ID/trace context.
- Add custom log messages in your APIs (`ILogger`) to confirm they appear in the trace.
### Topics to Learn

- What .NET Aspire is and how it replaces manual orchestration (like Docker Compose)
- Aspire Dashboard fundamentals (logs, traces, resource visualization)
- OpenTelemetry basics and how distributed tracing works
- Service references and dependency wiring in Aspire
- Understanding trace propagation between multiple services
- Simplified local orchestration of multi-service systems

### Useful links
- https://www.youtube.com/watch?v=DORZA_S7f9w what is Aspire?
- https://www.youtube.com/watch?v=nFU-hcHyl2s more about OpenTelemetry. How to populate the logs and metrics to Grafana and Prometheus when you don't have aspire dashboard.
