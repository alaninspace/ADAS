using System.Threading.Tasks;
using Xunit;
using Adas.Api.Services;

namespace Adas.Api.Tests;

public class MemoryStoreTests
{
    [Fact]
    public async Task MockMemoryStore_Should_Store_And_Retrieve_Documents()
    {
        IMemoryStore store = new MockMemoryStore();
        
        await store.SaveDocumentAsync("doc1", "This is a test document.");
        var doc = await store.GetDocumentAsync("doc1");
        
        Assert.Equal("This is a test document.", doc);
    }
}
