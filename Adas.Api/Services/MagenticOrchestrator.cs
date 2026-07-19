using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
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
            
        // Execute the workflow via MAF
        Run run = await InProcessExecution.RunAsync(workflow, inputPrompt);
        
        // Find the final output event
        var outputEvent = run.OutgoingEvents.OfType<WorkflowOutputEvent>().LastOrDefault();
        
        return outputEvent?.Data?.ToString() ?? "";
    }

    public async IAsyncEnumerable<string> StreamWorkflowAsync(string inputPrompt, string modelId, string agentId)
    {
        var chatClient = _clientFactory.CreateClient(modelId);
        
        var systemPrompt = agentId switch
        {
            "DbaAgent" => "You are a DBA expert.",
            "WindowsAdminAgent" => "You are a Windows Admin expert.",
            "LinuxAdminAgent" => "You are a Linux Admin expert.",
            _ => "You are a generic worker agent capable of solving technical problems."
        };

        var agent = new ChatClientAgent(chatClient, systemPrompt);
        
        // Build the workflow. In a real system, we'd use the selected agent as a participant or manager.
        Workflow workflow = new MagenticWorkflowBuilder(agent)
            .WithName($"Magentic Workflow - {agentId}")
            .RequirePlanSignoff(false)
            .WithMaxRounds(10)
            .Build();
            
        // Execute streaming workflow
        await using StreamingRun run = await InProcessExecution.Lockstep.RunStreamingAsync(workflow, inputPrompt);

        await foreach (var evt in run.WatchStreamAsync())
        {
            if (evt is AgentResponseUpdateEvent updateEvent && updateEvent.Data is ChatResponseUpdate update)
            {
                if (update.Text != null)
                {
                    yield return update.Text;
                }
            }
        }
    }
}
