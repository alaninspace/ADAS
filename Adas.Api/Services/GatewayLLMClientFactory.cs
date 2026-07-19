using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;
using System;

namespace Adas.Api.Services;

public class GatewayLLMClientFactory : ILLMClientFactory
{
    private readonly IConfiguration _configuration;

    public GatewayLLMClientFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IChatClient CreateClient(string modelId)
    {
        var endpoint = _configuration["Gateway:Endpoint"];
        var secret = _configuration["Gateway:ApiKey"] ?? _configuration["Gateway:Secret"] ?? "";
        
        var options = new OpenAIClientOptions();
        
        if (!string.IsNullOrEmpty(endpoint))
        {
            options.Endpoint = new Uri(endpoint);
        }

        // Use standard OpenAI Client pointing to the Gateway
        var openAiClient = new OpenAIClient(new ApiKeyCredential(secret), options);
        
        return openAiClient.GetChatClient(modelId).AsIChatClient();
    }

    // For testing purposes
    public Uri GetConfiguredEndpoint()
    {
        var endpoint = _configuration["Gateway:Endpoint"];
        return !string.IsNullOrEmpty(endpoint) ? new Uri(endpoint) : null;
    }
}
