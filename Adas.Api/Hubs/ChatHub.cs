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
}
