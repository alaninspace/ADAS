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

    public async Task<string> RunWorkflowAsync(string inputPrompt, CancellationToken cancellationToken = default)
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
        Run run = await InProcessExecution.RunAsync(workflow, inputPrompt, cancellationToken: cancellationToken);
        
        // Find the final output event
        var outputEvent = run.OutgoingEvents.OfType<WorkflowOutputEvent>().LastOrDefault();
        
        return outputEvent?.Data?.ToString() ?? "";
    }

    public async IAsyncEnumerable<string> StreamWorkflowAsync(string inputPrompt, string modelId, string agentId, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
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
        
        var managerAgent = new ChatClientAgent(chatClient, "You are a manager.");
        
        // Build the workflow. In a real system, we'd use the selected agent as a participant or manager.
        Workflow workflow = new MagenticWorkflowBuilder(managerAgent)
            .AddParticipants(new[] { agent })
            .WithName($"Magentic Workflow - {agentId}")
            .RequirePlanSignoff(false)
            .WithMaxRounds(10)
            .Build();
            
        var messages = new List<Microsoft.Extensions.AI.ChatMessage> 
        { 
            new(Microsoft.Extensions.AI.ChatRole.User, inputPrompt) 
        };

        // Execute streaming workflow
        await using StreamingRun run = await InProcessExecution.Lockstep.RunStreamingAsync(workflow, messages, cancellationToken: cancellationToken);
        await run.TrySendMessageAsync(new TurnToken(emitEvents: true));

        await foreach (var evt in run.WatchStreamAsync(cancellationToken))
        {
            if (evt is AgentResponseUpdateEvent updateEvent)
            {
                if (updateEvent.Update?.Text != null)
                {
                    yield return updateEvent.Update.Text;
                }
            }
            else if (evt is WorkflowErrorEvent errorEvent)
            {
                throw new Exception($"LLM Gateway Error: {errorEvent.Exception?.ToString()}");
            }
        }
    }
}
