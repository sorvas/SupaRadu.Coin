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
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Claude Reply")]
    public async Task<HttpResponseData> GetNegativeReply([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        try
        {
            var prompt = await new StreamReader(req.Body).ReadToEndAsync();
            
            _logger.LogInformation("Starting request to external service with prompt: {Prompt}", prompt);
            
            var responseMessage = await claudeApiClient.GetResponseAsync(prompt);
            
            _logger.LogInformation("Successfully received response from external service");
            
            _logger.LogInformation("Claude API response: {ResponseMessage}", responseMessage);

            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            await response.WriteStringAsync(responseMessage);
            
            return response;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HttpRequestException occurred: {ExMessage}", ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred: {ExMessage}", ex.Message);
            throw;
        }
    }
}