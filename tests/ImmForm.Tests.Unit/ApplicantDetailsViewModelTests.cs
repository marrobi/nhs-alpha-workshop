using System.ComponentModel.DataAnnotations;
using ImmForm.Web.Models;

namespace ImmForm.Tests.Unit;

[TestFixture]
public class ApplicantDetailsViewModelTests
{
    private static IList<ValidationResult> ValidateModel(ApplicantDetailsViewModel model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);
        Validator.TryValidateObject(model, context, results, validateAllProperties: true);
        return results;
    }

    [Test]
    public void ValidModel_ReturnsNoErrors()
    {
        var model = new ApplicantDetailsViewModel
        {
            FirstName = "Jane",
            Surname = "Smith",
            JobTitle = "Nurse Practitioner",
            Telephone = "01234 567890",
            Email = "jane.smith@nhs.net"
        };

        var results = ValidateModel(model);

        Assert.That(results, Is.Empty);
    }

    [Test]
    public void EmptyFirstName_ReturnsError()
    {
        var model = new ApplicantDetailsViewModel
        {
            FirstName = "",
            Surname = "Smith",
            JobTitle = "Nurse",
            Telephone = "01234 567890",
            Email = "jane@nhs.net"
        };

        var results = ValidateModel(model);

        Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
            r.ErrorMessage!.Contains("first name")));
    }

    [Test]
    public void EmptySurname_ReturnsError()
    {
        var model = new ApplicantDetailsViewModel
        {
            FirstName = "Jane",
            Surname = "",
            JobTitle = "Nurse",
            Telephone = "01234 567890",
            Email = "jane@nhs.net"
        };

        var results = ValidateModel(model);

        Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
            r.ErrorMessage!.Contains("surname")));
    }

    [Test]
    public void EmptyJobTitle_ReturnsError()
    {
        var model = new ApplicantDetailsViewModel
        {
            FirstName = "Jane",
            Surname = "Smith",
            JobTitle = "",
            Telephone = "01234 567890",
            Email = "jane@nhs.net"
        };

        var results = ValidateModel(model);

        Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
            r.ErrorMessage!.Contains("job title")));
    }

    [Test]
    public void EmptyTelephone_ReturnsError()
    {
        var model = new ApplicantDetailsViewModel
        {
            FirstName = "Jane",
            Surname = "Smith",
            JobTitle = "Nurse",
            Telephone = "",
            Email = "jane@nhs.net"
        };

        var results = ValidateModel(model);

        Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
            r.ErrorMessage!.Contains("telephone")));
    }

    [Test]
    public void EmptyEmail_ReturnsError()
    {
        var model = new ApplicantDetailsViewModel
        {
            FirstName = "Jane",
            Surname = "Smith",
            JobTitle = "Nurse",
            Telephone = "01234 567890",
            Email = ""
        };

        var results = ValidateModel(model);

        Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
            r.ErrorMessage!.Contains("email")));
    }

    [Test]
    public void InvalidEmailFormat_ReturnsError()
    {
        var model = new ApplicantDetailsViewModel
        {
            FirstName = "Jane",
            Surname = "Smith",
            JobTitle = "Nurse",
            Telephone = "01234 567890",
            Email = "not-an-email"
        };

        var results = ValidateModel(model);

        Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
            r.ErrorMessage!.Contains("email address in the correct format")));
    }

    [TestCase("noreply@nhs.net")]
    [TestCase("info@hospital.nhs.uk")]
    [TestCase("admin@clinic.nhs.uk")]
    [TestCase("team@trust.nhs.uk")]
    [TestCase("support@service.nhs.uk")]
    [TestCase("helpdesk@nhs.net")]
    [TestCase("enquiries@trust.nhs.uk")]
    [TestCase("contact@hospital.nhs.uk")]
    public void SharedMailboxEmail_ReturnsError(string email)
    {
        var model = new ApplicantDetailsViewModel
        {
            FirstName = "Jane",
            Surname = "Smith",
            JobTitle = "Nurse",
            Telephone = "01234 567890",
            Email = email
        };

        var results = ValidateModel(model);

        Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
            r.ErrorMessage!.Contains("Shared mailbox", StringComparison.OrdinalIgnoreCase)));
    }

    [Test]
    public void TelephoneTooShort_ReturnsError()
    {
        var model = new ApplicantDetailsViewModel
        {
            FirstName = "Jane",
            Surname = "Smith",
            JobTitle = "Nurse",
            Telephone = "012345",
            Email = "jane@nhs.net"
        };

        var results = ValidateModel(model);

        Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
            r.ErrorMessage!.Contains("telephone")));
    }

    [TestCase("01234 567890")]
    [TestCase("07700900982")]
    [TestCase("+44 1234 567890")]
    public void ValidTelephoneFormats_ReturnNoErrors(string telephone)
    {
        var model = new ApplicantDetailsViewModel
        {
            FirstName = "Jane",
            Surname = "Smith",
            JobTitle = "Nurse",
            Telephone = telephone,
            Email = "jane@nhs.net"
        };

        var results = ValidateModel(model);

        Assert.That(results, Is.Empty);
    }
}
