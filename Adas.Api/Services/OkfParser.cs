using Adas.Api.Models;

namespace Adas.Api.Services;

public class OkfParser : IOkfParser
{
    public OkfDocument Parse(string rawContent)
    {
        return new OkfDocument
        {
            Id = System.Guid.NewGuid().ToString(),
            Content = rawContent
        };
    }
}
