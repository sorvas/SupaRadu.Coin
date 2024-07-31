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
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile(Path.Combine(context.HostingEnvironment.ContentRootPath, "appsettings.json"), optional: true,
            reloadOnChange: false);
        config.AddJsonFile(
            Path.Combine(context.HostingEnvironment.ContentRootPath,
                $"appsettings.{Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT")}.json"),
            optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder => builder
                    .WithOrigins("https://green-river-09744ca03.5.azurestaticapps.net", "http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        
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

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    })
    .Build();

await host.RunAsync();