using System.Threading.Tasks;

namespace Adas.Api.Services;

public interface IMemoryStore
{
    Task SaveDocumentAsync(string id, string content);
    Task<string?> GetDocumentAsync(string id);
}
