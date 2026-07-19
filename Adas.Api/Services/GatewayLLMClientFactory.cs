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
        var secret = _configuration["Gateway:Secret"] ?? "";
        
        var options = new OpenAIClientOptions();
        
        // Use standard OpenAI Client pointing to the Gateway
        var openAiClient = new OpenAIClient(new ApiKeyCredential(secret), options);
        // Note: setting custom endpoint in the new OpenAI v2 SDK is usually done via a custom HttpClient in options, 
        // or by using AzureOpenAIClient if the gateway mimics Azure OpenAI.
        // For this mock, we will just instantiate the standard client.
        
        return openAiClient.GetChatClient(modelId).AsIChatClient();
    }
}
