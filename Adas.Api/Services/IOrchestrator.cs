using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adas.Api.Services;

public interface IOrchestrator
{
    Task<string> RunWorkflowAsync(string inputPrompt, CancellationToken cancellationToken = default);
    IAsyncEnumerable<string> StreamWorkflowAsync(string inputPrompt, string modelId, string agentId, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default);
}
