namespace Suparadu.Claude.ClientApiFunction.Models;

public class ClaudeApiSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string AnthropicVersion { get; set; } = string.Empty;
    public string ClaudeModel { get; set; } = string.Empty;
}