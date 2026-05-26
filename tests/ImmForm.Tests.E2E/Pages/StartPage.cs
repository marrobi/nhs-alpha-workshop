using Microsoft.Playwright;

namespace ImmForm.Tests.E2E.Pages;

public class StartPage
{
    private readonly IPage _page;

    public StartPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateAsync()
    {
        await _page.GotoAsync("/");
    }

    public async Task<string> GetHeadingTextAsync()
    {
        var heading = _page.GetByRole(AriaRole.Heading, new() { Level = 1 });
        return await heading.TextContentAsync() ?? string.Empty;
    }

    public async Task ClickStartNowAsync()
    {
        await _page.GetByRole(AriaRole.Button, new() { Name = "Start now" }).ClickAsync();
    }

    public async Task<bool> HasSkipLinkAsync()
    {
        var skipLink = _page.GetByRole(AriaRole.Link, new() { Name = "Skip to main content" });
        return await skipLink.IsVisibleAsync();
    }

    public async Task<bool> HasServiceNameAsync()
    {
        var serviceName = _page.GetByText("ImmForm Registration");
        return await serviceName.IsVisibleAsync();
    }
}
