using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.AI;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;

namespace Adas.Api.Services;

public class MagenticOrchestrator : IOrchestrator
{
    private readonly ILLMClientFactory _clientFactory;
    private readonly ILogger<MagenticOrchestrator> _logger;

    public MagenticOrchestrator(ILLMClientFactory clientFactory, ILogger<MagenticOrchestrator> logger)
    {
        _clientFactory = clientFactory;
        _logger = logger;
    }

    public async Task<string> RunWorkflowAsync(string inputPrompt)
    {
        var chatClient = _clientFactory.CreateClient("gpt-5.6-terra");
        
        var managerAgent = new ChatClientAgent(chatClient, "You are a manager orchestrating tasks for the ADAS platform.");
        var workerAgent = new ChatClientAgent(chatClient, "You are a generic worker agent capable of solving technical problems.");
        
        Workflow workflow = new MagenticWorkflowBuilder(managerAgent)
            .AddParticipants(new[] { workerAgent })
            .WithName("Magentic Orchestration Workflow")
            .WithDescription("Coordinates tasks")
            .RequirePlanSignoff(false)
            .WithMaxRounds(10)
            .WithMaxStalls(3)
            .WithMaxResets(2)
            .Build();
            
        var response = await chatClient.GetResponseAsync(inputPrompt); 
        return response?.ToString() ?? "";
    }
}
