using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs;
using ExecutionContext = Microsoft.Azure.WebJobs.ExecutionContext;
using Newtonsoft.Json;

using Orange.Data;

namespace Orange.Tests
{
    public class AlienTests
    {
        private readonly ITestOutputHelper output;
        private readonly ILogger logger = TestFactory.CreateLogger(LoggerTypes.List);

        public AlienTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async void GetPingWithSuccessResponseTest()
        {
            var request = TestFactory.CreateHttpRequest("name", "Bill");
            // string requestBody = await new StreamReader(request.Body).ReadToEndAsync();

            output.WriteLine(request.QueryString.ToString());

            var context = new ExecutionContext();
            var response = (OkObjectResult)await Ping.Run(request, context, logger);

            BasicResponse data = JsonConvert.DeserializeObject<BasicResponse>(response.Value.ToString());

            output.WriteLine(data.ToString());

            data.Application.Should().Contain("Orange Alien Functions");
        }

    }

}