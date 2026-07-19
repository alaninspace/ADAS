using Microsoft.Extensions.AI;

namespace Adas.Api.Services;

public interface ILLMClientFactory
{
    IChatClient CreateClient(string modelId);
}
