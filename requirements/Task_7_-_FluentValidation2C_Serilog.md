### Description

In this task, you’ll integrate three important backend concepts that improve application reliability and maintainability: **input validation**, **global error handling**, and **logging**.

The goal is to learn how to keep your controllers clean, validate input systematically, handle exceptions in one place, and log everything in a structured way using Serilog.

You will:

1. Add **FluentValidation** to validate incoming request models.
2. Implement a **Global Exception Handling Middleware** to catch all unhandled exceptions and return a structured JSON error response.
3. Configure **Serilog** to log validation errors, unhandled exceptions, and important request/response events.

- Install and configure **FluentValidation.AspNetCore**.
- Create validator classes for all request models.
- Register validators in the DI container. Use automatic registration, so that you don't need to specify each validator in DI separately. 
- Implement custom middleware for **global exception handling**:
    - Catch all exceptions from downstream.
    - Log details using Serilog.
    - Return a standardized error response. Use `ProblemDetails` format.
- Set up **Serilog** to:
    - Log to console.
    - Include request path, method, and status code.
### Topics to Learn

- Model validation using FluentValidation
- How to integrate FluentValidation into ASP.NET Core pipeline
- Middleware-based global error handling
- Centralized logging with Serilog
- Logging structured data and exceptions
- Separation of concerns: keeping controllers thin and clean 

## Useful links
- https://docs.fluentvalidation.net/en/latest/index.html - official FluentValidation documentation
- https://serilog.net/ - official Serilog documentation
- https://www.youtube.com/watch?v=-TGZypSinpw - about `ProblemDetails` and exception handling