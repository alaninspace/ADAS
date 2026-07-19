using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Adas.Api.Services;

public class MockMemoryStore : IMemoryStore
{
    private readonly ConcurrentDictionary<string, string> _store = new();

    public Task SaveDocumentAsync(string id, string content)
    {
        _store[id] = content;
        return Task.CompletedTask;
    }

    public Task<string?> GetDocumentAsync(string id)
    {
        _store.TryGetValue(id, out var content);
        return Task.FromResult(content);
    }
}
