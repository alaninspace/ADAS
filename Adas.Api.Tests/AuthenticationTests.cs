using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Adas.Api.Tests;

public class AuthenticationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AuthenticationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task SecureEndpoint_ReturnsUnauthorizedOrRedirect_WhenNoAuthBypass()
    {
        var client = _factory.WithWebHostBuilder(builder => 
        {
            builder.UseSetting("Authentication:BypassInDev", "false");
        }).CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        var response = await client.GetAsync("/api/user/info");
        
        Assert.True(response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Redirect, $"Status code was {response.StatusCode}. Content: {await response.Content.ReadAsStringAsync()}");
    }
}
