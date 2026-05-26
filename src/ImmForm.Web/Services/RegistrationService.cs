using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using ImmForm.Web.Data;
using ImmForm.Web.Models;

namespace ImmForm.Web.Services;

public interface IRegistrationService
{
    Task<string> SubmitRegistrationAsync(RegistrationSession session, string declarationFullName, string declarationJobTitle, CancellationToken cancellationToken);
}

public class RegistrationService : IRegistrationService
{
    private readonly ImmFormDbContext _dbContext;

    public RegistrationService(ImmFormDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> SubmitRegistrationAsync(RegistrationSession session, string declarationFullName, string declarationJobTitle, CancellationToken cancellationToken)
    {
        var correlationId = GenerateCorrelationId();
        var now = DateTimeOffset.UtcNow;

        var registration = new Registration
        {
            Id = Guid.NewGuid(),
            CorrelationId = correlationId,
            FirstName = session.FirstName!,
            Surname = session.Surname!,
            JobTitle = session.JobTitle!,
            Telephone = session.Telephone!,
            Email = session.Email!,
            AccountNumber = session.AccountNumber!,
            OrganisationCode = session.OrganisationCode!,
            OrganisationName = session.OrganisationName!,
            AuthorisedPersonName = session.AuthorisedPersonName!,
            AuthorisedPersonEmail = session.AuthorisedPersonEmail!,
            Status = RegistrationStatus.Submitted,
            DeclarationFullName = declarationFullName,
            DeclarationJobTitle = declarationJobTitle,
            DeclarationTimestamp = now,
            SubmittedAt = now,
            CreatedAt = now,
            UpdatedAt = now
        };

        registration.PayloadChecksum = ComputePayloadChecksum(registration);

        _dbContext.Registrations.Add(registration);

        var auditLog = new AuditLog
        {
            RegistrationId = registration.Id,
            CorrelationId = correlationId,
            EventType = "EVT-02",
            Timestamp = now,
            ActorType = "Applicant",
            ActorId = session.Email!,
            PreviousState = null,
            NewState = "Submitted",
            Detail = JsonSerializer.Serialize(new { Event = "Submission received" })
        };

        _dbContext.AuditLogs.Add(auditLog);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return correlationId;
    }

    private static string GenerateCorrelationId()
    {
        var bytes = RandomNumberGenerator.GetBytes(8);
        return $"IMM-{Convert.ToHexString(bytes)[..12].ToUpperInvariant()}";
    }

    private static string ComputePayloadChecksum(Registration registration)
    {
        var payload = JsonSerializer.Serialize(new
        {
            registration.FirstName,
            registration.Surname,
            registration.JobTitle,
            registration.Telephone,
            registration.Email,
            registration.AccountNumber,
            registration.OrganisationCode,
            registration.OrganisationName,
            registration.DeclarationFullName,
            registration.DeclarationJobTitle,
            registration.DeclarationTimestamp
        });

        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(payload));
        return Convert.ToHexString(hash);
    }
}
