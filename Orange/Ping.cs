using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orange.Data;

namespace Orange
{
    public static class Ping
    {
        private static string _invocationId;

        [FunctionName("Ping")]
        [OpenApiOperation(operationId: "Ping", tags: new[] { "Ping" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "Returns JSON response")]
        public static async Task<IActionResult> Run([
            HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/ping")] HttpRequest req,
            ExecutionContext executionContext, ILogger log)
        {
            _invocationId = executionContext.InvocationId.ToString();

            await Task.Yield();

            BasicResponse pingResponse = new()
            {
                InvocationId = _invocationId,
                Application = "Orange Alien Functions",
                Message = "Ping Response",
                InvocationDate = DateTimeOffset.UtcNow
            };

            return (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(pingResponse));
        }
    }
}
