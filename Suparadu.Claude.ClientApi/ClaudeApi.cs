using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Suparadu.Claude.ClientApiFunction;

public class ClaudeApi(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<ClaudeApi>();

    [Function("GetNegativeReply")]
    public IActionResult GetNegativeReply([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation($"{nameof(GetNegativeReply)} HTTP trigger function processed a request");

        HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

        response.WriteString("Claude API called!");

        return new OkObjectResult(response);
    }
}