using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs;
using ExecutionContext = Microsoft.Azure.WebJobs.ExecutionContext;
using Newtonsoft.Json;

namespace Orange.Tests
{
    public class AlienTests
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void GetPingWithSuccessResponseTest()
        {
            var request = TestFactory.CreateHttpRequest("name", "Bill");
            var context = new ExecutionContext();
            var response = (OkObjectResult)await Ping.Run(request, context, logger);

            PingResponse data = JsonConvert.DeserializeObject<PingResponse>(response.Value.ToString());

            data.Application.Should().Contain("Orange Alien Functions");
        }
    }

    public record PingResponse
    {
        public string InvocationId { get; init; }
        public string Application { get; init; }
        public string Message { get; init; }
        public DateTimeOffset InvocationDate { get; init; }
    }

}