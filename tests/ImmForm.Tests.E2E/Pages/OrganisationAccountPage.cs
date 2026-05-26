using Microsoft.Playwright;

namespace ImmForm.Tests.E2E.Pages;

public class OrganisationAccountPage
{
    private readonly IPage _page;

    public OrganisationAccountPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateAsync()
    {
        await _page.GotoAsync("/register/organisation-account");
    }

    public async Task<string> GetHeadingTextAsync()
    {
        var heading = _page.GetByRole(AriaRole.Heading, new() { Level = 1 });
        return await heading.TextContentAsync() ?? string.Empty;
    }

    public async Task FillAccountNumberAsync(string value)
    {
        await _page.GetByLabel("ImmForm account number").FillAsync(value);
    }

    public async Task FillOrganisationCodeAsync(string value)
    {
        await _page.GetByLabel("ImmForm organisation code").FillAsync(value);
    }

    public async Task FillFormAsync(string accountNumber, string organisationCode)
    {
        await FillAccountNumberAsync(accountNumber);
        await FillOrganisationCodeAsync(organisationCode);
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

    public async Task<bool> HasOrganisationConfirmationAsync()
    {
        var insetText = _page.GetByText("Riverside NHS Trust");
        return await insetText.IsVisibleAsync();
    }
}
