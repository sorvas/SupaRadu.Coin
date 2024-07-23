using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Suparadu.Claude.ClientApiFunction.Models;

namespace Suparadu.Claude.ClientApiFunction.Services;

public class ClaudeApiClient : IClaudeApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ClaudeApiSettings _claudeApiSettings;

    public ClaudeApiClient(
        HttpClient httpClient, 
        IOptions<JsonSerializerOptions> jsonOptions,
        IOptions<ClaudeApiSettings> settings)
    {
        _httpClient = httpClient;
        _jsonOptions = jsonOptions.Value;
        _claudeApiSettings = settings.Value;

        _httpClient.DefaultRequestHeaders.Add(Constants.ClaudeApiHeaders.ApiKey, _claudeApiSettings.ApiKey);
        _httpClient.DefaultRequestHeaders.Add(Constants.ClaudeApiHeaders.AnthropicVersion, _claudeApiSettings.AnthropicVersion);
    }

    public async Task<string> GetResponseAsync(string prompt)
    {
        ClaudeApiRequest request = new()
        {
            Model = _claudeApiSettings.ClaudeModel,
            MaxTokens = 1000,
            Messages =
            [
                new Message
                {
                    Role = "user", 
                    Content = prompt
                }
            ],
            System = "BASE SYSTEM BEHAVIOR PROMPT",
            Temperature = 0.7m
        };

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_claudeApiSettings.BaseUrl, request, _jsonOptions);
        response.EnsureSuccessStatusCode();

        ClaudeApiResponse? responseObject = await response.Content.ReadFromJsonAsync<ClaudeApiResponse>(_jsonOptions);

        return responseObject?.Content[0].Text ?? string.Empty;
    }
}

public class ClaudeApiRequest
{
    [JsonPropertyName("model")] 
    public string Model { get; set; } = string.Empty;
    [JsonPropertyName("max_tokens")] 
    public int MaxTokens { get; set; }
    [JsonPropertyName("messages")] 
    public Message[] Messages { get; set; } = [];

    [JsonPropertyName("system")]
    public string System { get; set; } = string.Empty;
    [JsonPropertyName("temperature")]
    public decimal Temperature { get; set; }
    
}

public class Message
{
    [JsonPropertyName("role")] 
    public string Role { get; set; } = string.Empty;

    [JsonPropertyName("content")] 
    public string Content { get; set; } = string.Empty;
}

public record ClaudeApiResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    
    [JsonPropertyName("content")] 
    public ContentBlock[] Content { get; set; } = [];

    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;
}

public record ContentBlock
{
    [JsonPropertyName("type")] 
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("text")] 
    public string Text { get; set; } = string.Empty;
}

