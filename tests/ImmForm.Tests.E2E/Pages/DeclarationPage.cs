using Microsoft.Playwright;

namespace ImmForm.Tests.E2E.Pages;

public class DeclarationPage
{
    private readonly IPage _page;

    public DeclarationPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateAsync()
    {
        await _page.GotoAsync("/register/declaration");
    }

    public async Task<string> GetHeadingTextAsync()
    {
        var heading = _page.GetByRole(AriaRole.Heading, new() { Level = 1 });
        return await heading.TextContentAsync() ?? string.Empty;
    }

    public async Task CheckDeclarationAsync()
    {
        await _page.GetByRole(AriaRole.Checkbox).CheckAsync();
    }

    public async Task ClickSubmitAsync()
    {
        await _page.GetByRole(AriaRole.Button, new() { Name = "Submit application" }).ClickAsync();
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
}
