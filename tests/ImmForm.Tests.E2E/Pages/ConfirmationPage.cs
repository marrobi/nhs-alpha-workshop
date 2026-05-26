using Microsoft.Playwright;

namespace ImmForm.Tests.E2E.Pages;

public class ConfirmationPage
{
    private readonly IPage _page;

    public ConfirmationPage(IPage page)
    {
        _page = page;
    }

    public async Task<string> GetPanelHeadingAsync()
    {
        var panelHeading = _page.GetByRole(AriaRole.Heading, new() { Level = 1 });
        return await panelHeading.TextContentAsync() ?? string.Empty;
    }

    public async Task<bool> HasReferenceNumberAsync()
    {
        var referenceText = _page.GetByText("IMM-");
        return await referenceText.IsVisibleAsync();
    }

    public async Task<string> GetReferenceNumberAsync()
    {
        var referenceText = _page.Locator(".govuk-panel__body strong");
        return await referenceText.TextContentAsync() ?? string.Empty;
    }

    public async Task<bool> HasWhatHappensNextAsync()
    {
        var heading = _page.GetByRole(AriaRole.Heading, new() { Name = "What happens next" });
        return await heading.IsVisibleAsync();
    }

    public async Task<bool> HasNoErrorSummaryAsync()
    {
        var errorSummary = _page.GetByRole(AriaRole.Alert);
        return !(await errorSummary.IsVisibleAsync());
    }
}
