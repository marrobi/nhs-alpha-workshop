using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImmForm.Web.Data;

[Table("AuditLogs", Schema = "audit")]
public class AuditLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public Guid RegistrationId { get; set; }

    [Required]
    [MaxLength(50)]
    public string CorrelationId { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string EventType { get; set; } = string.Empty;

    public DateTimeOffset Timestamp { get; set; }

    [Required]
    [MaxLength(50)]
    public string ActorType { get; set; } = string.Empty;

    [Required]
    [MaxLength(254)]
    public string ActorId { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? PreviousState { get; set; }

    [MaxLength(50)]
    public string? NewState { get; set; }

    public string? Detail { get; set; }

    [MaxLength(64)]
    public string? HashedIPAddress { get; set; }
}
