namespace Suparadu.Claude.ClientApiFunction.Services;

public interface IClaudeApiClient
{
    Task<string> GetResponseAsync(string prompt, string model = "claude-3-sonnet-20240229");
}