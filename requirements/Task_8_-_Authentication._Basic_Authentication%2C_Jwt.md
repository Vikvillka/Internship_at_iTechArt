### Description

In this task, you’ll implement user authentication and authorization using **Basic Authentication** and **JWT (JSON Web Tokens)**.  
You’ll create two controllers — `UserController` and `AuthController` — and a `User` entity persisted in the database.  
The goal is to learn how to manage users, hash passwords securely, protect endpoints, and issue JWTs with refresh tokens.

Create `User` entity with fields:`Id`, `Username`, `PasswordHash` and (optional) `CreatedAt`, `UpdatedAt`. No need to create separate microservice for that - that will be implemented later.

**UserController**
- `POST /users` – **Public** endpoint to create a new user.
    - Accepts username and password.
    - Password must be **hashed** using a secure algorithm (`SHA256`, `PBKDF2`, or `BCrypt`).
- `DELETE /users/{id}` – **Protected** with **Basic Authentication**.
    - User must provide correct `Authorization` header.
    - Deletes the authenticated user from the database.

**AuthController**
- `POST /auth/getTokens` – Accepts `username` and `password`.
    - Validate credentials against the database (generate hash to compare it with the hash in database).
    - Return an access token and a refresh token.
    - Access token should expire after a short time (e.g., 5–10 minutes), Refresh token - after 2 hours or so
- `POST /auth/refresh` – Accepts a valid refresh token.
    - Return a new access token (no DB check needed).
    - No need to persist tokens in the database.

Support both auth methods (basic and jwt) using AuthenticationSchemes.

All other endpoints that you've done before should be protected with JWT auth.

Verify your authorization works with Postman. Check "Authorization" tab in your request, "Basic Auth" and "Bearer Token" methods. Create postman variables for {{authToken}}, {{userName}}, {{password}}. Set the {{authToken}} value automatically when the `getTokens` request  is completed.
### Topics to Learn
- ASP.NET Core Authentication & Authorization pipeline
- Custom `AuthenticationHandler` (Basic Auth)
- Password hashing and verification
- JWT token structure (header, payload, signature)
- Token expiration and refresh flow
- Protecting endpoints with `[Authorize]`

### Useful links
- https://www.youtube.com/watch?v=mgeuh8k3I4g - to help you with JWT auth.
- https://medium.com/@ritvanramhajdari/using-multiple-authentication-schemes-in-net-core-07e3d014105d - about multiple auth schemes 