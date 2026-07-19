using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Xunit;
using Adas.Api.Services;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Adas.Api.Tests;

public class LLMClientFactoryTests
{
    [Fact]
    public void Factory_Creates_Client_With_Correct_Configuration()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                {"Gateway:Endpoint", "https://gateway.mock.com/"},
                {"Gateway:Secret", "mock-secret"}
            })
            .Build();

        var factory = new GatewayLLMClientFactory(config);
        var client = factory.CreateClient("gpt-4");
        
        Assert.NotNull(client);
    }

    [Fact]
    public void Factory_Configures_OpenAIClient_Endpoint_To_Gateway()
    {
        // This is a failing test to ensure we properly point to the Foundry API Gateway
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                {"Gateway:Endpoint", "https://gateway.mock.com/api/v1/"},
                {"Gateway:Secret", "mock-secret"}
            })
            .Build();

        var factory = new GatewayLLMClientFactory(config);
        
        // Expose endpoint or assert its usage
        var endpoint = factory.GetConfiguredEndpoint();
        Assert.Equal("https://gateway.mock.com/api/v1/", endpoint.ToString());
    }
}
