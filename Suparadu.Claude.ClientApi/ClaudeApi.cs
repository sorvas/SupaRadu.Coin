using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Suparadu.Claude.ClientApiFunction.Services;

namespace Suparadu.Claude.ClientApiFunction;

public class ClaudeApi(ILoggerFactory loggerFactory, IClaudeApiClient claudeApiClient)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<ClaudeApi>();

    [Function("GetNegativeReply")]
    public async Task<IActionResult> GetNegativeReply([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation($"{nameof(GetNegativeReply)} HTTP trigger function processed a request");

        var prompt = await new StreamReader(req.Body).ReadToEndAsync();
        
        var responseMessage = await claudeApiClient.GetResponseAsync(prompt);

        return new OkObjectResult(responseMessage);
    }
}