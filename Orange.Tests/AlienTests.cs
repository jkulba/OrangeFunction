using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs;
using ExecutionContext = Microsoft.Azure.WebJobs.ExecutionContext;

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
            Assert.Contains("Orange Alien Functions", response.Value);
        }
    }
}