using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using Xunit;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Adas.Api.Services;

namespace Adas.Api.Tests;

public class ChatHubTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ChatHubTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private class MockOrchestrator : IOrchestrator
    {
        public Task<string> RunWorkflowAsync(string inputPrompt, CancellationToken cancellationToken = default)
        {
            return Task.FromResult("Response");
        }

        public async IAsyncEnumerable<string> StreamWorkflowAsync(string inputPrompt, string modelId, string agentId, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            yield return "Chunk 1";
            await Task.Delay(10, cancellationToken);
            yield return "Chunk 2";
            await Task.Delay(10, cancellationToken);
            yield return "Chunk 3";
        }
    }

    [Fact]
    public async Task ChatHub_Can_Connect_And_Receive_Message()
    {
        var server = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IOrchestrator, MockOrchestrator>();
            });
        }).Server;
        
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost/chathub", o =>
            {
                o.HttpMessageHandlerFactory = _ => server.CreateHandler();
            })
            .Build();

        string? receivedMessage = null;
        connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            receivedMessage = message;
        });

        await connection.StartAsync();
        Assert.Equal(HubConnectionState.Connected, connection.State);
    }

    [Fact]
    public async Task ChatHub_Streams_Tokens_Chunk_By_Chunk()
    {
        var server = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IOrchestrator, MockOrchestrator>();
            });
        }).Server;

        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost/chathub", o =>
            {
                o.HttpMessageHandlerFactory = _ => server.CreateHandler();
            })
            .Build();

        await connection.StartAsync();

        var tokens = new List<string>();
        connection.On<string>("ReceiveToken", token =>
        {
            tokens.Add(token);
        });

        connection.On<Microsoft.AspNetCore.Mvc.ProblemDetails>("ReceiveError", problem =>
        {
            Assert.Fail($"Received error: {problem.Title} - {problem.Detail}");
        });

        await connection.SendAsync("StreamMessage", "Hello", "gpt-4", "GeneralAgent");
        
        await Task.Delay(500); // Wait for streaming to finish

        Assert.NotEmpty(tokens);
        Assert.True(tokens.Count == 3, "Expected 3 chunked tokens");
    }
}
