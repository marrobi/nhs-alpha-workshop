extern alias WebApp;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using WebProgram = WebApp::Program;

namespace ImmForm.Tests.Integration;

[TestFixture]
public class RegistrationFlowTests
{
    private WebApplicationFactory<WebProgram> _factory = null!;
    private HttpClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = new WebApplicationFactory<WebProgram>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddInMemoryCollection(new Dictionary<string, string?>
                    {
                        ["OrganisationApi:BaseUrl"] = "http://localhost:9999"
                    });
                });
            });
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task ApplicantDetails_Get_Returns200()
    {
        var response = await _client.GetAsync("/register/applicant-details");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task ApplicantDetails_Get_ContainsFormFields()
    {
        var response = await _client.GetAsync("/register/applicant-details");
        var content = await response.Content.ReadAsStringAsync();

        Assert.That(content, Does.Contain("FirstName"));
        Assert.That(content, Does.Contain("Surname"));
        Assert.That(content, Does.Contain("JobTitle"));
        Assert.That(content, Does.Contain("Telephone"));
        Assert.That(content, Does.Contain("Email"));
    }

    [Test]
    public async Task ApplicantDetails_Get_ContainsHeading()
    {
        var response = await _client.GetAsync("/register/applicant-details");
        var content = await response.Content.ReadAsStringAsync();

        Assert.That(content, Does.Contain("Your details"));
    }

    [Test]
    public async Task ApplicantDetails_PostValid_RedirectsToOrganisationAccount()
    {
        // Create a client that handles cookies (for session + antiforgery)
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
            HandleCookies = true
        });

        // First get the page to get the antiforgery token and session cookie
        var getResponse = await client.GetAsync("/register/applicant-details");
        var getContent = await getResponse.Content.ReadAsStringAsync();
        var token = ExtractAntiForgeryToken(getContent);

        var formData = new Dictionary<string, string>
        {
            ["FirstName"] = "Jane",
            ["Surname"] = "Smith",
            ["JobTitle"] = "Nurse Practitioner",
            ["Telephone"] = "01234 567890",
            ["Email"] = "jane.smith@nhs.net",
            ["__RequestVerificationToken"] = token
        };

        var response = await client.PostAsync("/register/applicant-details", new FormUrlEncodedContent(formData));

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Redirect));
        Assert.That(response.Headers.Location?.ToString(), Is.EqualTo("/register/organisation-account"));

        client.Dispose();
    }

    [Test]
    public async Task ApplicantDetails_PostEmpty_ReturnsViewWithErrors()
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
            HandleCookies = true
        });

        var getResponse = await client.GetAsync("/register/applicant-details");
        var getContent = await getResponse.Content.ReadAsStringAsync();
        var token = ExtractAntiForgeryToken(getContent);

        var formData = new Dictionary<string, string>
        {
            ["FirstName"] = "",
            ["Surname"] = "",
            ["JobTitle"] = "",
            ["Telephone"] = "",
            ["Email"] = "",
            ["__RequestVerificationToken"] = token
        };

        var response = await client.PostAsync("/register/applicant-details", new FormUrlEncodedContent(formData));
        var content = await response.Content.ReadAsStringAsync();

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(content, Does.Contain("There is a problem"));

        client.Dispose();
    }

    [Test]
    public async Task OrganisationAccount_Get_Returns200()
    {
        var response = await _client.GetAsync("/register/organisation-account");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task OrganisationAccount_Get_ContainsFormFields()
    {
        var response = await _client.GetAsync("/register/organisation-account");
        var content = await response.Content.ReadAsStringAsync();

        Assert.That(content, Does.Contain("AccountNumber"));
        Assert.That(content, Does.Contain("OrganisationCode"));
    }

    [Test]
    public async Task CheckYourAnswers_GetWithNoSession_RedirectsToStart()
    {
        var response = await _client.GetAsync("/register/check-your-answers");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Redirect));
        Assert.That(response.Headers.Location?.ToString(), Is.EqualTo("/"));
    }

    [Test]
    public async Task Declaration_GetWithNoSession_RedirectsToStart()
    {
        var response = await _client.GetAsync("/register/declaration");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Redirect));
        Assert.That(response.Headers.Location?.ToString(), Is.EqualTo("/"));
    }

    private static string ExtractAntiForgeryToken(string html)
    {
        var tokenStart = html.IndexOf("name=\"__RequestVerificationToken\" type=\"hidden\" value=\"", StringComparison.Ordinal);
        if (tokenStart == -1)
        {
            tokenStart = html.IndexOf("__RequestVerificationToken\" type=\"hidden\" value=\"", StringComparison.Ordinal);
            if (tokenStart == -1) return string.Empty;
            tokenStart = html.IndexOf("value=\"", tokenStart, StringComparison.Ordinal) + 7;
        }
        else
        {
            tokenStart += "name=\"__RequestVerificationToken\" type=\"hidden\" value=\"".Length;
        }

        var tokenEnd = html.IndexOf("\"", tokenStart, StringComparison.Ordinal);
        return html[tokenStart..tokenEnd];
    }
}
