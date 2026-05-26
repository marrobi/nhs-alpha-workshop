using Microsoft.Playwright;

namespace ImmForm.Tests.E2E.Pages;

public class ApplicantDetailsPage
{
    private readonly IPage _page;

    public ApplicantDetailsPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateAsync()
    {
        await _page.GotoAsync("/register/applicant-details");
    }

    public async Task<string> GetHeadingTextAsync()
    {
        var heading = _page.GetByRole(AriaRole.Heading, new() { Level = 1 });
        return await heading.TextContentAsync() ?? string.Empty;
    }

    public async Task FillFirstNameAsync(string value)
    {
        await _page.GetByLabel("First name").FillAsync(value);
    }

    public async Task FillSurnameAsync(string value)
    {
        await _page.GetByLabel("Surname").FillAsync(value);
    }

    public async Task FillJobTitleAsync(string value)
    {
        await _page.GetByLabel("Job title").FillAsync(value);
    }

    public async Task FillTelephoneAsync(string value)
    {
        await _page.GetByLabel("Telephone number").FillAsync(value);
    }

    public async Task FillEmailAsync(string value)
    {
        await _page.GetByLabel("Email address").FillAsync(value);
    }

    public async Task FillFormAsync(string firstName, string surname, string jobTitle, string telephone, string email)
    {
        await FillFirstNameAsync(firstName);
        await FillSurnameAsync(surname);
        await FillJobTitleAsync(jobTitle);
        await FillTelephoneAsync(telephone);
        await FillEmailAsync(email);
    }

    public async Task ClickContinueAsync()
    {
        await _page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }

    public async Task<bool> HasBackLinkAsync()
    {
        var backLink = _page.GetByRole(AriaRole.Link, new() { Name = "Back" });
        return await backLink.IsVisibleAsync();
    }

    public async Task<bool> HasErrorSummaryAsync()
    {
        var errorSummary = _page.GetByRole(AriaRole.Alert);
        return await errorSummary.IsVisibleAsync();
    }

    public async Task<string> GetErrorSummaryTextAsync()
    {
        var errorSummary = _page.GetByRole(AriaRole.Alert);
        return await errorSummary.TextContentAsync() ?? string.Empty;
    }
}
