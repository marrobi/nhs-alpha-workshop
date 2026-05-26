var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

app.MapPost("/api/mock/organisation/validate", (OrganisationValidateRequest request) =>
{
    if (request.AccountNumber == "1234567890" && request.OrganisationCode == "ABC123")
    {
        return Results.Ok(new OrganisationValidateResponse(
            IsValid: true,
            OrganisationName: "Riverside NHS Trust",
            AuthorisedPersonName: "Dr Sarah Thompson",
            AuthorisedPersonEmail: "sarah.thompson@riverside.nhs.uk"));
    }

    return Results.Ok(new OrganisationValidateResponse(
        IsValid: false,
        OrganisationName: null,
        AuthorisedPersonName: null,
        AuthorisedPersonEmail: null));
});

app.MapGet("/api/mock/registration/check-duplicate", (string email) =>
{
    var duplicateEmails = new[] { "duplicate@test.nhs.uk" };
    var isDuplicate = duplicateEmails.Contains(email, StringComparer.OrdinalIgnoreCase);
    return Results.Ok(new DuplicateCheckResponse(IsDuplicate: isDuplicate));
});

app.MapPost("/api/mock/registration/create", (RegistrationCreateRequest request) =>
{
    return Results.Ok(new RegistrationCreateResponse(
        Success: true,
        UserId: Guid.NewGuid().ToString()));
});

app.Run();

public partial class Program { }

public record OrganisationValidateRequest(string AccountNumber, string OrganisationCode);
public record OrganisationValidateResponse(bool IsValid, string? OrganisationName, string? AuthorisedPersonName, string? AuthorisedPersonEmail);
public record DuplicateCheckResponse(bool IsDuplicate);
public record RegistrationCreateRequest(string Email, string FirstName, string Surname, string AccountNumber, string OrganisationCode);
public record RegistrationCreateResponse(bool Success, string? UserId);
