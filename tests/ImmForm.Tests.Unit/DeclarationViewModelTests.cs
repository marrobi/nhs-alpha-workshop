using System.ComponentModel.DataAnnotations;
using ImmForm.Web.Models;

namespace ImmForm.Tests.Unit;

[TestFixture]
public class DeclarationViewModelTests
{
    private static IList<ValidationResult> ValidateModel(DeclarationViewModel model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);
        Validator.TryValidateObject(model, context, results, validateAllProperties: true);
        return results;
    }

    [Test]
    public void ValidModel_ReturnsNoErrors()
    {
        var model = new DeclarationViewModel
        {
            FullName = "Jane Smith",
            DeclarationJobTitle = "Nurse",
            ConfirmDeclaration = true
        };

        var results = ValidateModel(model);

        Assert.That(results, Is.Empty);
    }

    [Test]
    public void UncheckedDeclaration_ReturnsError()
    {
        var model = new DeclarationViewModel
        {
            FullName = "Jane Smith",
            DeclarationJobTitle = "Nurse",
            ConfirmDeclaration = false
        };

        var results = ValidateModel(model);

        Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
            r.ErrorMessage!.Contains("confirm the declaration")));
    }

    [Test]
    public void EmptyFullName_ReturnsError()
    {
        var model = new DeclarationViewModel
        {
            FullName = "",
            DeclarationJobTitle = "Nurse",
            ConfirmDeclaration = true
        };

        var results = ValidateModel(model);

        Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
            r.ErrorMessage!.Contains("full name")));
    }

    [Test]
    public void EmptyJobTitle_ReturnsError()
    {
        var model = new DeclarationViewModel
        {
            FullName = "Jane Smith",
            DeclarationJobTitle = "",
            ConfirmDeclaration = true
        };

        var results = ValidateModel(model);

        Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
            r.ErrorMessage!.Contains("job title")));
    }
}
