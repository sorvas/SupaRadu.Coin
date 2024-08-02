using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Suparadu.Claude.ClientApiFunction.Services;

namespace Suparadu.Claude.ClientApiFunction;

public class ClaudeApi(ILoggerFactory loggerFactory, IClaudeApiClient claudeApiClient)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<ClaudeApi>();

    [Function("GetNegativeReply")]
    [OpenApiOperation(operationId: "GetNegativeReply", tags: ["name"])]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<HttpResponseData> GetNegativeReply([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var prompt = await new StreamReader(req.Body).ReadToEndAsync();
        _logger.LogInformation("Received prompt: {Prompt}", prompt);
        
        var responseMessage = await claudeApiClient.GetResponseAsync(prompt);
        _logger.LogInformation("Claude API response: {ResponseMessage}", responseMessage);

        HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        await response.WriteStringAsync(responseMessage);

        return response;
    }
}