using Xunit;
using Adas.Api.Services;

namespace Adas.Api.Tests;

public class OkfParserTests
{
    [Fact]
    public void OkfParser_Should_Parse_RawContent_To_Document()
    {
        var parser = new OkfParser();
        var rawContent = "Some OKF content";
        
        var doc = parser.Parse(rawContent);
        
        Assert.NotNull(doc.Id);
        Assert.Equal(rawContent, doc.Content);
    }
}
