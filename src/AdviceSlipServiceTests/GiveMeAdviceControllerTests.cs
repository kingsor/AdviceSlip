using AdviceSlipService.Models;
using AdviceSlipServiceTests.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AdviceSlipServiceTests
{
    public class GiveMeAdviceControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly HttpClient _client;

        public GiveMeAdviceControllerTests(WebApplicationFactory<Program> application)
        {
            _client = application.CreateClient();
        }

        [Theory]
        [InlineData("life", 5)]
        [InlineData("world", 5)]
        [InlineData("love", 2)]
        [InlineData("mother", 2)]
        public async Task Post_RetrievesAdviceSlipWithParams(string topic, int amount)
        {
            // Arrange
            var jsonBody = $"{{\"topic\": \"{topic}\", \"amount\": {amount}}}";

            //Act
            var adviceResponse = await _client.PostAndDeserialize<AdviceResponse>("/GiveMeAdvice", jsonBody);

            // Assert
            Assert.NotNull(adviceResponse);
        }

        [Theory]
        [InlineData("", 5)]
        public async Task Post_CheckValidationOnMissingTopic(string topic, int amount)
        {
            // Arrange
            var jsonBody = $"{{\"topic\": \"{topic}\", \"amount\": {amount}}}";

            //Act
            var response = await _client.PostAsync("/GiveMeAdvice", jsonBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("life")]
        [InlineData("world")]
        [InlineData("love")]
        [InlineData("mother")]
        public async Task Post_RetrievesAdviceSlipWithoutAmount(string topic)
        {
            // Arrange
            var jsonBody = $"{{\"topic\": \"{topic}\"}}";

            //Act
            var response = await _client.PostAsync("/GiveMeAdvice", jsonBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Post_CheckErrorOnEmptyBodyObject()
        {
            // Arrange
            var jsonBody = "{}";

            //Act
            var response = await _client.PostAsync("/GiveMeAdvice", jsonBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_CheckErrorOnEmptyBody()
        {
            // Arrange
            var jsonBody = "";

            //Act
            var response = await _client.PostAsync("/GiveMeAdvice", jsonBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
