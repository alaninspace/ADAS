using System.Threading.Tasks;

namespace Adas.Api.Services;

public interface IOrchestrator
{
    Task<string> RunWorkflowAsync(string inputPrompt);
}
