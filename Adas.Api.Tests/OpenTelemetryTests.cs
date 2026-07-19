using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Xunit;

namespace Adas.Api.Tests;

public class OpenTelemetryTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public OpenTelemetryTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void OpenTelemetry_IsConfigured_InServiceCollection()
    {
        var services = _factory.Services;
        
        var tracerProvider = services.GetService<TracerProvider>();
        Assert.NotNull(tracerProvider);

        var meterProvider = services.GetService<MeterProvider>();
        Assert.NotNull(meterProvider);
    }
}
