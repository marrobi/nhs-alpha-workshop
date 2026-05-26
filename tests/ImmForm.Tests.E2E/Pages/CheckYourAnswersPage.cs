using Microsoft.Playwright;

namespace ImmForm.Tests.E2E.Pages;

public class CheckYourAnswersPage
{
    private readonly IPage _page;

    public CheckYourAnswersPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateAsync()
    {
        await _page.GotoAsync("/register/check-your-answers");
    }

    public async Task<string> GetHeadingTextAsync()
    {
        var heading = _page.GetByRole(AriaRole.Heading, new() { Level = 1 });
        return await heading.TextContentAsync() ?? string.Empty;
    }

    public async Task<bool> HasSummaryValueAsync(string text)
    {
        var value = _page.GetByText(text, new() { Exact = true });
        return await value.IsVisibleAsync();
    }

    public async Task<bool> HasChangeLinkAsync(string fieldName)
    {
        var changeLink = _page.GetByRole(AriaRole.Link, new() { Name = $"Change {fieldName}" });
        return await changeLink.IsVisibleAsync();
    }

    public async Task ClickChangeLinkAsync(string fieldName)
    {
        await _page.GetByRole(AriaRole.Link, new() { Name = $"Change {fieldName}" }).ClickAsync();
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
}
