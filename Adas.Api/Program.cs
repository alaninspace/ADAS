using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Adas.Api.Services.IMemoryStore, Adas.Api.Services.MockMemoryStore>();
builder.Services.AddSingleton<Adas.Api.Services.IOkfParser, Adas.Api.Services.OkfParser>();

var otel = builder.Services.AddOpenTelemetry();
otel.ConfigureResource(resource => resource.AddService("Adas.Api"));

var otelEnabled = builder.Configuration.GetValue<bool>("Observability:Enabled");
var otlpEndpoint = builder.Configuration.GetValue<string>("Observability:OtlpEndpoint");

otel.WithTracing(tracing =>
{
    tracing.AddAspNetCoreInstrumentation();
    if (otelEnabled && !string.IsNullOrEmpty(otlpEndpoint))
    {
        tracing.AddOtlpExporter(opt => opt.Endpoint = new Uri(otlpEndpoint));
    }
});

otel.WithMetrics(metrics =>
{
    metrics.AddAspNetCoreInstrumentation();
    if (otelEnabled && !string.IsNullOrEmpty(otlpEndpoint))
    {
        metrics.AddOtlpExporter(opt => opt.Endpoint = new Uri(otlpEndpoint));
    }
});

if (otelEnabled && !string.IsNullOrEmpty(otlpEndpoint))
{
    builder.Logging.AddOpenTelemetry(logging => 
    {
        logging.IncludeFormattedMessage = true;
        logging.IncludeScopes = true;
        logging.AddOtlpExporter(opt => opt.Endpoint = new Uri(otlpEndpoint));
    });
}

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public partial class Program { }
