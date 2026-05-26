using System.ComponentModel.DataAnnotations;

namespace ImmForm.Web.Models;

public class ApplicantDetailsViewModel : IValidatableObject
{
    [Required(ErrorMessage = "Enter your first name")]
    [MaxLength(100, ErrorMessage = "First name must be 100 characters or fewer")]
    [Display(Name = "First name")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Enter your surname")]
    [MaxLength(100, ErrorMessage = "Surname must be 100 characters or fewer")]
    [Display(Name = "Surname")]
    public string? Surname { get; set; }

    [Required(ErrorMessage = "Enter your job title")]
    [MaxLength(100, ErrorMessage = "Job title must be 100 characters or fewer")]
    [Display(Name = "Job title")]
    public string? JobTitle { get; set; }

    [Required(ErrorMessage = "Enter your telephone number")]
    [MaxLength(20, ErrorMessage = "Telephone number must be 20 characters or fewer")]
    [Display(Name = "Telephone number")]
    public string? Telephone { get; set; }

    [Required(ErrorMessage = "Enter your email address")]
    [MaxLength(254, ErrorMessage = "Email address must be 254 characters or fewer")]
    [Display(Name = "Email address")]
    public string? Email { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrWhiteSpace(Email))
        {
            if (!IsValidEmailFormat(Email))
            {
                yield return new ValidationResult(
                    "Enter an email address in the correct format, like name@example.com",
                    [nameof(Email)]);
            }
            else if (IsSharedMailbox(Email))
            {
                yield return new ValidationResult(
                    "Enter an individual email address. Shared mailboxes cannot be used for ImmForm registration.",
                    [nameof(Email)]);
            }
        }

        if (!string.IsNullOrWhiteSpace(Telephone) && !IsValidUkTelephone(Telephone))
        {
            yield return new ValidationResult(
                "Enter a telephone number, like 01632 960 001 or 07700 900 982",
                [nameof(Telephone)]);
        }
    }

    private static bool IsValidEmailFormat(string email)
    {
        var trimmed = email.Trim();
        if (string.IsNullOrWhiteSpace(trimmed))
            return false;

        var atIndex = trimmed.IndexOf('@');
        if (atIndex <= 0 || atIndex == trimmed.Length - 1)
            return false;

        var local = trimmed[..atIndex];
        var domain = trimmed[(atIndex + 1)..];

        if (string.IsNullOrWhiteSpace(local) || string.IsNullOrWhiteSpace(domain))
            return false;

        if (!domain.Contains('.'))
            return false;

        if (domain.StartsWith('.') || domain.EndsWith('.'))
            return false;

        return true;
    }

    private static bool IsSharedMailbox(string email)
    {
        var local = email.Trim().Split('@')[0].ToLowerInvariant();
        string[] sharedPrefixes = ["noreply", "no-reply", "info", "admin", "team", "support", "helpdesk", "enquiries", "contact"];
        return sharedPrefixes.Contains(local);
    }

    private static bool IsValidUkTelephone(string telephone)
    {
        var digitsOnly = new string(telephone.Where(char.IsDigit).ToArray());
        var stripped = telephone.Trim().TrimStart('+');

        if (stripped.Any(c => !char.IsDigit(c) && c != ' ' && c != '-'))
            return false;

        return digitsOnly.Length >= 10 && digitsOnly.Length <= 15;
    }
}
