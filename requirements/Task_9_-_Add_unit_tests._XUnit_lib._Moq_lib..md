### Description

In this task, you’ll create **unit tests** for your **service layer** using **xUnit** and **Moq**.  
The goal is to verify your business logic independently of the database, controllers, or HTTP layer.  
Since your services already depend on repository interfaces, you’ll mock those repositories to isolate and test only service behavior.
### Requirements

Create a new test project named `<YourApp>.Tests` (if not already existing).
Use NuGet packages XUnit and Moq

Write unit tests **only** for service classes (no controller or repository tests). All services and all their methods should be covered by tests. Cover both “happy path” and “failure” cases.
Test exception scenarios with `Assert.ThrowsAsync<T>()`.
Do not use FluentAssertions or other Assertion libraries please! Just XUnit `Assert`.

Mock all dependencies using **Moq** (e.g., `IRepository<T>`, `ILogger<T>`, or others your service depends on). For each dependency, verify it was called as a part of your unit test (for example, make sure your service method called repository method while executing).

Each test should follow the **Arrange–Act–Assert** pattern.
Include both `[Fact]` and `[Theory]` tests: `[Fact]` for single-scenario tests. `[Theory]` for parameterized tests (e.g., testing invalid inputs, edge cases).

Mind the file structure and methods names while creating unit tests. I suggest next pattern: `<YourTestsProject>/ServicesTests/<ServiceName>Tests/<MethodName>Tests.cs/<Case>`
Example: `Tests/ServicesTests/UserServiceTests/CreateUserTests.cs/ShouldCreateUser__WhenInputDataIsValid()`.
You can use other approaches, but make sure it's readable, maintainable and correlates with any common standard.

Using your IDE or other tools, generate coverage report and provide it in the ticket. Don't include it in your PR please. It can be screenshot of your IDE, or command console, or any other file - just something showing that you covered your Services project.

### Topics to Learn
- Fundamentals of unit testing and isolation
- xUnit basics (`[Fact]`, `[Theory]`, `[InlineData]`)
- Mocking dependencies using Moq (`Setup`, `Returns`, `Verify`)
- Testing async methods (`async Task` tests)
- Verifying that service logic correctly calls repository methods
- Writing clear and maintainable test names
### Useful links
- https://www.youtube.com/watch?v=9ZvDBSQa_so - unit tests examples, Moq library explained.
- https://www.youtube.com/watch?v=xwMWGYD8rgk - code coverage tips 
 