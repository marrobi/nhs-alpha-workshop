using System.ComponentModel.DataAnnotations;

namespace ImmForm.Web.Models;

public class DeclarationViewModel : IValidatableObject
{
    [Display(Name = "Full name")]
    [Required(ErrorMessage = "Enter your full name")]
    [MaxLength(200, ErrorMessage = "Full name must be 200 characters or fewer")]
    public string? FullName { get; set; }

    [Display(Name = "Job title")]
    [Required(ErrorMessage = "Enter your job title")]
    [MaxLength(100, ErrorMessage = "Job title must be 100 characters or fewer")]
    public string? DeclarationJobTitle { get; set; }

    [Display(Name = "Declaration confirmation")]
    public bool ConfirmDeclaration { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!ConfirmDeclaration)
        {
            yield return new ValidationResult(
                "You must confirm the declaration before submitting",
                [nameof(ConfirmDeclaration)]);
        }
    }
}
