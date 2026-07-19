using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Adas.Api.Services;

namespace Adas.Api.Hubs;

public class ChatHub : Hub
{
    private readonly IOrchestrator _orchestrator;

    public ChatHub(IOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    public async Task SendMessage(string message)
    {
        var response = await _orchestrator.RunWorkflowAsync(message);
        await Clients.Caller.SendAsync("ReceiveMessage", "Manager", response);
    }

    public async Task StreamMessage(string message, string modelId, string agentId)
    {
        var cancellationToken = Context.ConnectionAborted;
        try
        {
            await foreach (var token in _orchestrator.StreamWorkflowAsync(message, modelId, agentId, cancellationToken))
            {
                await Clients.Caller.SendAsync("ReceiveToken", token);
            }
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            Console.WriteLine($"[ChatHub] Caught Exception: {ex.GetType().Name} - {ex.Message}");
            var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Title = "Error communicating with LLM Gateway",
                Detail = ex.Message,
                Status = 500
            };
            
            await Clients.Caller.SendAsync("ReceiveError", problemDetails);
        }
        finally
        {
            await Clients.Caller.SendAsync("StreamComplete");
        }
    }
}
