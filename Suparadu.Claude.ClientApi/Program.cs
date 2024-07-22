using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Suparadu.Claude.ClientApiFunction.Models;
using Suparadu.Claude.ClientApiFunction.Services;

IHost host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((_, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.Configure<ClaudeApiSettings>(context.Configuration.GetSection("ClaudeApi"));
        
        services.Configure<JsonSerializerOptions>(options =>
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
        
        services.AddHttpClient<ClaudeApiClient>((serviceProvider, client) =>
        {
            ClaudeApiSettings settings = serviceProvider.GetRequiredService<IOptions<ClaudeApiSettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseUrl);
        });

        services.AddScoped<IClaudeApiClient, ClaudeApiClient>();
    })
    .Build();

await host.RunAsync();