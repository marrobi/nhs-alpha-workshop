using System.ComponentModel.DataAnnotations;
using ImmForm.Web.Models;

namespace ImmForm.Tests.Unit;

[TestFixture]
public class OrganisationAccountViewModelTests
{
    private static IList<ValidationResult> ValidateModel(OrganisationAccountViewModel model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);
        Validator.TryValidateObject(model, context, results, validateAllProperties: true);
        return results;
    }

    [Test]
    public void ValidModel_ReturnsNoErrors()
    {
        var model = new OrganisationAccountViewModel
        {
            AccountNumber = "1234567890",
            OrganisationCode = "ABC123"
        };

        var results = ValidateModel(model);

        Assert.That(results, Is.Empty);
    }

    [Test]
    public void EmptyAccountNumber_ReturnsError()
    {
        var model = new OrganisationAccountViewModel
        {
            AccountNumber = "",
            OrganisationCode = "ABC123"
        };

        var results = ValidateModel(model);

        Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
            r.ErrorMessage!.Contains("account number")));
    }

    [Test]
    public void EmptyOrganisationCode_ReturnsError()
    {
        var model = new OrganisationAccountViewModel
        {
            AccountNumber = "1234567890",
            OrganisationCode = ""
        };

        var results = ValidateModel(model);

        Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
            r.ErrorMessage!.Contains("organisation code")));
    }

    [TestCase("12345")]
    [TestCase("12345678901")]
    [TestCase("abcdefghij")]
    public void InvalidAccountNumber_ReturnsError(string accountNumber)
    {
        var model = new OrganisationAccountViewModel
        {
            AccountNumber = accountNumber,
            OrganisationCode = "ABC123"
        };

        var results = ValidateModel(model);

        Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
            r.ErrorMessage!.Contains("10 digits")));
    }

    [Test]
    public void ValidTenDigitAccountNumber_ReturnsNoErrors()
    {
        var model = new OrganisationAccountViewModel
        {
            AccountNumber = "9876543210",
            OrganisationCode = "XYZ456"
        };

        var results = ValidateModel(model);

        Assert.That(results, Is.Empty);
    }
}
