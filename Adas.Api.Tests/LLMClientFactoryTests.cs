using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Xunit;
using Adas.Api.Services;

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
}
