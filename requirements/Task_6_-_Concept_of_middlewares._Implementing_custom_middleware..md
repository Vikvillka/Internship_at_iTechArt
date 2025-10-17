### Description

Middleware allows us to intercept requests as they pass through the ASP.NET Core pipeline. In this task, you’ll build a custom middleware that measures how long each request takes to execute. The goal is to understand middleware structure, async flow, and how data can be shared inside the request pipeline.

Your middleware should:

- Start a timer when the request begins.
- Execute the next middleware in the pipeline.
- Stop the timer after the response is sent.
- Store the start datetime in `HttpContext.Items["RequestDuration"]`.
- Add the duration to the response headers under the key `X-Request-Duration`.

### Topics to learn:

- ASP.NET Core middleware architecture and request pipeline
- Using `Stopwatch` to measure execution time
- Understanding async middleware (`await _next(context)`)
- Adding custom headers to HTTP responses
- Using `HttpContext.Items` for cross-component data sharing

## Useful links
- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-9.0 - official Microsoft documentation
