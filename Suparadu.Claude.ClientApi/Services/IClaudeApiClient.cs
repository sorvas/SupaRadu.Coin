namespace Suparadu.Claude.ClientApiFunction.Services;

public interface IClaudeApiClient
{
    Task<string> GetResponseAsync(string prompt);
}