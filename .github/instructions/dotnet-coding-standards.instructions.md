---
applyTo: "**"
---

# .NET Coding Standards (C# / ASP.NET Core)

See `tech-stack.instructions.md` for the current technology choices. The rules below apply to all C# code in this repository and are derived from the [Microsoft C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).

---

## Naming Conventions

- **PascalCase**: classes, interfaces, methods, properties, events, namespaces, public fields
- **camelCase**: local variables, method parameters
- **`_camelCase`** (underscore prefix): private instance fields — e.g. `private readonly ILogger _logger;`
- **`I` prefix**: interfaces — e.g. `IRegistrationService`, `IFormStep<T>`
- **`Async` suffix**: all async methods — e.g. `GetAccountAsync()`, `SubmitRegistrationAsync()`
- **PascalCase for acronyms of 3+ characters**: `HttpClient` not `HTTPClient`; `ImmFormApi` not `ImmFormAPI`

---

## Nullability

- Enable nullable reference types in every project: `<Nullable>enable</Nullable>` in `*.csproj`
- Never use the null-forgiving operator (`!`) without a code comment explaining why the value cannot be null
- Prefer `string?` for optional string values over empty-string sentinels
- Check for null at public API boundaries — do not assume non-null inside private methods

---

## Type Safety

- Never use `var` on public method return types — always declare the return type explicitly on public API surfaces
- Use `var` freely for local variables where the type is obvious from the right-hand side
- Use `record` types for immutable DTOs and value objects
- Use `IReadOnlyList<T>` or `IEnumerable<T>` for read-only collections in public API surfaces
- Never use `dynamic` or `object` where a concrete or generic type can be used

---

## Async / Await

- All I/O-bound methods must be `async` and return `Task` or `Task<T>`
- Pass `CancellationToken` through all public async method signatures
- Never use `.Result` or `.Wait()` — always `await`
- Use `ConfigureAwait(false)` in library code; not required in ASP.NET Core controllers

---

## Input Validation

- Validate all inbound data at the controller boundary using Data Annotations (`[Required]`, `[MaxLength]`, `[RegularExpression]`) or FluentValidation
- Check `ModelState.IsValid` in MVC controller actions before processing; `[ApiController]` auto-validates on Web API controllers and returns 400 for invalid models
- Never pass user input to `Process.Start`, raw SQL strings, or `@Html.Raw()` with user-supplied content
- Use EF Core parameterised queries only — never string-interpolate user data into LINQ or raw SQL

---

## Configuration

- Access configuration via `IConfiguration` or strongly-typed `IOptions<T>` — e.g. `builder.Configuration["Section:Key"]`
- Never use `.GetValue<T>("Key", defaultValue)` for secrets or required configuration — missing values must throw at startup
- Load secrets from Azure Key Vault via Managed Identity in production; environment variables in development
- Never hardcode connection strings, API keys, or other secrets in source code

---

## Formatting

- Enforce consistent code style with EditorConfig (`.editorconfig` in the repository root)
- Run `dotnet format` to apply EditorConfig formatting rules automatically
- .NET Roslyn analysers enforce additional style and correctness rules — CI must fail on analyser warnings treated as errors
- CI runs `dotnet format --verify-no-changes` to block unformatted code from merging

---

## File Organisation

- One public type per file; file name matches the type name
- Namespace matches the folder path relative to the project root
- Place interfaces alongside their primary implementation, not in a separate `Interfaces/` folder
