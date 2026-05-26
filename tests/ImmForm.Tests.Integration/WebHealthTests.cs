extern alias WebApp;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using WebProgram = WebApp::Program;

namespace ImmForm.Tests.Integration;

[TestFixture]
public class WebHealthTests
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
        _client = _factory.CreateClient();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task HealthEndpoint_Get_Returns200Ok()
    {
        var response = await _client.GetAsync("/health");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task StartPage_Get_Returns200Ok()
    {
        var response = await _client.GetAsync("/");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task StartPage_Get_ContainsServiceName()
    {
        var response = await _client.GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();

        Assert.That(content, Does.Contain("Register as a new orderer on an existing ImmForm account"));
    }

    [Test]
    public async Task StartPage_Get_ContainsStartButton()
    {
        var response = await _client.GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();

        Assert.That(content, Does.Contain("Start now"));
    }

    [Test]
    public async Task StartPage_Get_ContainsSkipLink()
    {
        var response = await _client.GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();

        Assert.That(content, Does.Contain("Skip to main content"));
    }

    [Test]
    public async Task StartPage_Get_ContainsWhatYouWillNeed()
    {
        var response = await _client.GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();

        Assert.That(content, Does.Contain("What you will need"));
        Assert.That(content, Does.Contain("10-digit ImmForm account number"));
        Assert.That(content, Does.Contain("organisation code"));
        Assert.That(content, Does.Contain("professional email address"));
        Assert.That(content, Does.Contain("job title"));
        Assert.That(content, Does.Contain("telephone number"));
    }

    [Test]
    public async Task StartPage_Get_ContainsApAutoLookupStatement()
    {
        var response = await _client.GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();

        Assert.That(content, Does.Contain("looked up automatically"));
    }

    [Test]
    public async Task StartPage_Get_ContainsProcessingTime()
    {
        var response = await _client.GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();

        Assert.That(content, Does.Contain("2 working days"));
    }

    [Test]
    public async Task StartPage_Get_ContainsOtherWaysToRegister()
    {
        var response = await _client.GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();

        Assert.That(content, Does.Contain("Other ways to register"));
        Assert.That(content, Does.Contain("helpdesk@immform.org.uk"));
    }
}
