using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImmForm.Web.Data;

public enum RegistrationStatus
{
    Draft,
    Submitted,
    AwaitingApproval,
    Approved,
    Rejected,
    Expired,
    AccountCreated,
    Qualified,
    QualificationRejected
}

[Table("Registrations", Schema = "dbo")]
public class Registration
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string CorrelationId { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Surname { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string JobTitle { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Telephone { get; set; } = string.Empty;

    [Required]
    [MaxLength(254)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string AccountNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string OrganisationCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string OrganisationName { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string AuthorisedPersonName { get; set; } = string.Empty;

    [Required]
    [MaxLength(254)]
    public string AuthorisedPersonEmail { get; set; } = string.Empty;

    public RegistrationStatus Status { get; set; } = RegistrationStatus.Draft;

    [MaxLength(200)]
    public string? DeclarationFullName { get; set; }

    [MaxLength(100)]
    public string? DeclarationJobTitle { get; set; }

    public DateTimeOffset? DeclarationTimestamp { get; set; }

    public DateTimeOffset? SubmittedAt { get; set; }

    public DateTimeOffset? ApprovedAt { get; set; }

    public DateTimeOffset? RejectedAt { get; set; }

    public DateTimeOffset? ActivatedAt { get; set; }

    [MaxLength(1000)]
    public string? RejectionReason { get; set; }

    public int ResendCount { get; set; }

    [MaxLength(64)]
    public string? PayloadChecksum { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
