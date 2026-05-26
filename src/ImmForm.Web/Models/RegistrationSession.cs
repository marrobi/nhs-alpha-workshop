namespace ImmForm.Web.Models;

public class RegistrationSession
{
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    public string? JobTitle { get; set; }
    public string? Telephone { get; set; }
    public string? Email { get; set; }
    public string? AccountNumber { get; set; }
    public string? OrganisationCode { get; set; }
    public string? OrganisationName { get; set; }
    public string? AuthorisedPersonName { get; set; }
    public string? AuthorisedPersonEmail { get; set; }
    public bool IsSubmitted { get; set; }
    public string? CorrelationId { get; set; }
    public bool ReturnToCheckYourAnswers { get; set; }
}
