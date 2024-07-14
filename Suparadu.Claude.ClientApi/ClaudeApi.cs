using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Suparadu.Claude.ClientApiFunction;

public class ClaudeApi(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<ClaudeApi>();

    [Function("ClaudeApi")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request");

        HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

        response.WriteString("Welcome to Azure Functions!");

        return new OkObjectResult(response);
    }
}