using Deque.AxeCore.Commons;
using Deque.AxeCore.Playwright;
using ImmForm.Tests.E2E.Pages;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace ImmForm.Tests.E2E.Journeys;

[TestFixture]
public class RegistrationHappyPathTests : PageTest
{
    private StartPage _startPage = null!;
    private ApplicantDetailsPage _applicantDetailsPage = null!;
    private OrganisationAccountPage _organisationAccountPage = null!;
    private CheckYourAnswersPage _checkYourAnswersPage = null!;
    private DeclarationPage _declarationPage = null!;
    private ConfirmationPage _confirmationPage = null!;

    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions
        {
            BaseURL = TestContext.Parameters.Get("BaseUrl", "http://localhost:5080")
        };
    }

    [SetUp]
    public void SetUpPages()
    {
        _startPage = new StartPage(Page);
        _applicantDetailsPage = new ApplicantDetailsPage(Page);
        _organisationAccountPage = new OrganisationAccountPage(Page);
        _checkYourAnswersPage = new CheckYourAnswersPage(Page);
        _declarationPage = new DeclarationPage(Page);
        _confirmationPage = new ConfirmationPage(Page);
    }

    [Test]
    public async Task FullRegistrationJourney_HappyPath()
    {
        // Step 1: Start page - view checklist and click Start now
        await _startPage.NavigateAsync();
        var heading = await _startPage.GetHeadingTextAsync();
        Assert.That(heading, Does.Contain("Register as a new orderer"));
        await AssertAccessibilityAsync("Start page");
        await Page.ScreenshotAsync(new() { Path = "tests/ImmForm.Tests.E2E/Screenshots/01-start-page.png" });

        await _startPage.ClickStartNowAsync();
        await Page.WaitForURLAsync("**/register/applicant-details");

        // Step 2: Applicant details - fill form
        var applicantHeading = await _applicantDetailsPage.GetHeadingTextAsync();
        Assert.That(applicantHeading, Does.Contain("Your details"));
        Assert.That(await _applicantDetailsPage.HasBackLinkAsync(), Is.True);
        await AssertAccessibilityAsync("Applicant details page");

        await _applicantDetailsPage.FillFormAsync(
            firstName: "Priya",
            surname: "Chandrasekaran",
            jobTitle: "Vaccination Coordinator",
            telephone: "020 7946 0958",
            email: "priya.chandrasekaran@nhs.net"
        );
        await Page.ScreenshotAsync(new() { Path = "tests/ImmForm.Tests.E2E/Screenshots/02-applicant-details-filled.png" });
        await _applicantDetailsPage.ClickContinueAsync();
        await Page.WaitForURLAsync("**/register/organisation-account");

        // Step 3: Organisation account - enter account number and org code
        var orgHeading = await _organisationAccountPage.GetHeadingTextAsync();
        Assert.That(orgHeading, Does.Contain("Organisation and account"));
        Assert.That(await _organisationAccountPage.HasBackLinkAsync(), Is.True);
        await AssertAccessibilityAsync("Organisation account page");

        await _organisationAccountPage.FillFormAsync(
            accountNumber: "1234567890",
            organisationCode: "ABC123"
        );
        await Page.ScreenshotAsync(new() { Path = "tests/ImmForm.Tests.E2E/Screenshots/03-organisation-account-filled.png" });
        await _organisationAccountPage.ClickContinueAsync();
        await Page.WaitForURLAsync("**/register/check-your-answers");

        // Step 4: Check your answers - verify data displayed
        var cyaHeading = await _checkYourAnswersPage.GetHeadingTextAsync();
        Assert.That(cyaHeading, Does.Contain("Check your answers"));
        Assert.That(await _checkYourAnswersPage.HasSummaryValueAsync("Priya"), Is.True);
        Assert.That(await _checkYourAnswersPage.HasSummaryValueAsync("Chandrasekaran"), Is.True);
        Assert.That(await _checkYourAnswersPage.HasSummaryValueAsync("priya.chandrasekaran@nhs.net"), Is.True);
        Assert.That(await _checkYourAnswersPage.HasSummaryValueAsync("1234567890"), Is.True);
        await AssertAccessibilityAsync("Check your answers page");
        await Page.ScreenshotAsync(new() { Path = "tests/ImmForm.Tests.E2E/Screenshots/04-check-your-answers.png" });

        await _checkYourAnswersPage.ClickContinueAsync();
        await Page.WaitForURLAsync("**/register/declaration");

        // Step 5: Declaration - confirm and submit
        var declarationHeading = await _declarationPage.GetHeadingTextAsync();
        Assert.That(declarationHeading, Does.Contain("Declaration"));
        Assert.That(await _declarationPage.HasBackLinkAsync(), Is.True);
        await AssertAccessibilityAsync("Declaration page");

        await _declarationPage.CheckDeclarationAsync();
        await Page.ScreenshotAsync(new() { Path = "tests/ImmForm.Tests.E2E/Screenshots/05-declaration-confirmed.png" });
        await _declarationPage.ClickSubmitAsync();
        await Page.WaitForURLAsync("**/register/confirmation/**");

        // Step 6: Confirmation - verify reference number
        var confirmHeading = await _confirmationPage.GetPanelHeadingAsync();
        Assert.That(confirmHeading, Does.Contain("Application submitted"));
        Assert.That(await _confirmationPage.HasReferenceNumberAsync(), Is.True);
        Assert.That(await _confirmationPage.HasWhatHappensNextAsync(), Is.True);
        Assert.That(await _confirmationPage.HasNoErrorSummaryAsync(), Is.True);
        await AssertAccessibilityAsync("Confirmation page");
        await Page.ScreenshotAsync(new() { Path = "tests/ImmForm.Tests.E2E/Screenshots/06-confirmation.png" });
    }

    [Test]
    public async Task RegistrationJourney_ValidationErrors_ApplicantDetails()
    {
        // Navigate to applicant details and submit empty form
        await _applicantDetailsPage.NavigateAsync();
        await _applicantDetailsPage.ClickContinueAsync();

        // Should show error summary
        Assert.That(await _applicantDetailsPage.HasErrorSummaryAsync(), Is.True);
        var errorText = await _applicantDetailsPage.GetErrorSummaryTextAsync();
        Assert.That(errorText, Does.Contain("first name"));
        await AssertAccessibilityAsync("Applicant details validation errors");
        await Page.ScreenshotAsync(new() { Path = "tests/ImmForm.Tests.E2E/Screenshots/07-applicant-errors.png" });
    }

    [Test]
    public async Task RegistrationJourney_ChangeLink_ReturnsToCheckYourAnswers()
    {
        // Complete the flow up to check your answers first
        await _startPage.NavigateAsync();
        await _startPage.ClickStartNowAsync();

        await _applicantDetailsPage.FillFormAsync(
            firstName: "Jane",
            surname: "Smith",
            jobTitle: "Practice Nurse",
            telephone: "0115 496 0123",
            email: "jane.smith@nhs.net"
        );
        await _applicantDetailsPage.ClickContinueAsync();

        await _organisationAccountPage.FillFormAsync("1234567890", "ABC123");
        await _organisationAccountPage.ClickContinueAsync();
        await Page.WaitForURLAsync("**/register/check-your-answers");

        // Click a Change link
        await _checkYourAnswersPage.ClickChangeLinkAsync("first name");
        await Page.WaitForURLAsync("**/register/applicant-details**");

        // Update the value
        await _applicantDetailsPage.FillFirstNameAsync("Janet");
        await _applicantDetailsPage.ClickContinueAsync();

        // Should return to check your answers with updated value
        await Page.WaitForURLAsync("**/register/check-your-answers");
        Assert.That(await _checkYourAnswersPage.HasSummaryValueAsync("Janet"), Is.True);
        await Page.ScreenshotAsync(new() { Path = "tests/ImmForm.Tests.E2E/Screenshots/08-changed-answer.png" });
    }

    private async Task AssertAccessibilityAsync(string pageName)
    {
        var axeResult = await Page.RunAxe();
        var violations = axeResult.Violations ?? Array.Empty<AxeResultItem>();
        var violationMessage = FormatViolations(violations);
        Assert.That(
            violations,
            Is.Empty,
            $"Accessibility violations on '{pageName}': {violationMessage}"
        );
    }

    private static string FormatViolations(AxeResultItem[] violations)
    {
        if (violations.Length == 0) return "None";
        var messages = violations.Select(v => $"{v.Id}: {v.Description} ({v.Nodes.Length} instances)");
        return string.Join("; ", messages);
    }
}
