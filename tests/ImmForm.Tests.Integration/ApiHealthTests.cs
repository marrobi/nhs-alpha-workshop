extern alias ApiApp;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using ApiProgram = ApiApp::Program;

namespace ImmForm.Tests.Integration;

[TestFixture]
public class ApiHealthTests
{
    private WebApplicationFactory<ApiProgram> _factory = null!;
    private HttpClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = new WebApplicationFactory<ApiProgram>();
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
    public async Task HealthEndpoint_Get_ReturnsHealthyStatus()
    {
        var response = await _client.GetAsync("/health");
        var content = await response.Content.ReadAsStringAsync();

        Assert.That(content, Is.EqualTo("Healthy"));
    }
}
