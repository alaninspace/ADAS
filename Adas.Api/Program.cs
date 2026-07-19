using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;
using Adas.Api.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Adas.Api.Services.IMemoryStore, Adas.Api.Services.MockMemoryStore>();
builder.Services.AddSingleton<Adas.Api.Services.IOkfParser, Adas.Api.Services.OkfParser>();
builder.Services.AddSingleton<Adas.Api.Services.ILLMClientFactory, Adas.Api.Services.GatewayLLMClientFactory>();

builder.Services.AddControllers();
builder.Services.AddSignalR();
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

builder.Services.AddOpenApi();

var authBypass = builder.Configuration.GetValue<bool>("Authentication:BypassInDev");

var authBuilder = builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = authBypass ? CookieAuthenticationDefaults.AuthenticationScheme : OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(options => 
{
    options.Cookie.Name = "Adas.Auth";
    options.Cookie.SameSite = SameSiteMode.Strict;
});

if (!authBypass)
{
    authBuilder.AddOpenIdConnect(options =>
    {
        var authConfig = builder.Configuration.GetSection("Authentication:Schemes:MicrosoftOidc");
        options.Authority = authConfig["Authority"];
        options.ClientId = authConfig["ClientId"];
        options.ResponseType = "code";
        options.SaveTokens = true;
        options.TokenValidationParameters.RoleClaimType = AdasAuthorization.RoleClaimType;
    });
}

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AdasAuthorization.OperatorPolicy, policy => 
    {
        if (authBypass)
        {
            policy.RequireAssertion(context => true);
        }
        else 
        {
            policy.RequireAuthenticatedUser();
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/user/info", (System.Security.Claims.ClaimsPrincipal user) => 
{
    return new { user.Identity?.Name, IsAuthenticated = user.Identity?.IsAuthenticated ?? true };
}).RequireAuthorization(Adas.Api.Security.AdasAuthorization.OperatorPolicy);

app.MapHub<Adas.Api.Hubs.ChatHub>("/chathub");

app.MapGet("/login", (string? returnUrl, HttpContext context) =>
{
    return Results.Challenge(new Microsoft.AspNetCore.Authentication.AuthenticationProperties { RedirectUri = returnUrl ?? "/" }, 
        new[] { OpenIdConnectDefaults.AuthenticationScheme });
});

app.MapGet("/logout", (HttpContext context) =>
{
    return Results.SignOut(new Microsoft.AspNetCore.Authentication.AuthenticationProperties { RedirectUri = "/" }, 
        new[] { CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme });
});

app.MapFallbackToFile("index.html");

app.Run();

public partial class Program { }
