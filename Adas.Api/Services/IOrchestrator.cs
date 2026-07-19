using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adas.Api.Services;

public interface IOrchestrator
{
    Task<string> RunWorkflowAsync(string inputPrompt);
    IAsyncEnumerable<string> StreamWorkflowAsync(string inputPrompt, string modelId, string agentId);
}
