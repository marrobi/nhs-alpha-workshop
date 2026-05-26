using System.ComponentModel.DataAnnotations;

namespace ImmForm.Web.Models;

public class OrganisationAccountViewModel : IValidatableObject
{
    [Required(ErrorMessage = "Enter your ImmForm account number")]
    [Display(Name = "ImmForm account number")]
    public string? AccountNumber { get; set; }

    [Required(ErrorMessage = "Enter your ImmForm organisation code")]
    [MaxLength(50, ErrorMessage = "Organisation code must be 50 characters or fewer")]
    [Display(Name = "ImmForm organisation code")]
    public string? OrganisationCode { get; set; }

    public string? OrganisationName { get; set; }

    public string? ApiErrorMessage { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrWhiteSpace(AccountNumber))
        {
            var digitsOnly = new string(AccountNumber.Where(char.IsDigit).ToArray());
            if (digitsOnly.Length != 10 || AccountNumber.Trim().Length != 10 || digitsOnly != AccountNumber.Trim())
            {
                yield return new ValidationResult(
                    "Enter a valid ImmForm account number. It is 10 digits long.",
                    [nameof(AccountNumber)]);
            }
        }
    }
}
