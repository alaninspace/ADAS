using Adas.Api.Models;

namespace Adas.Api.Services;

public interface IOkfParser
{
    OkfDocument Parse(string rawContent);
}
