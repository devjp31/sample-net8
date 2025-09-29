using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sample_net8.Controllers;
using System.Linq;
using Xunit;

namespace sample_net8.Tests
{
    public class WeatherForecastControllerTests
    {
        private readonly WeatherForecastController _controller;

        public WeatherForecastControllerTests()
        {
            var logger = new LoggerFactory().CreateLogger<WeatherForecastController>();
            _controller = new WeatherForecastController(logger);
        }

        [Fact]
        public void Get_ShouldReturnFiveItems()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public void Get_ShouldContainValidTemperatureRange()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.All(result, item =>
            {
                Assert.InRange(item.TemperatureC, -20, 55);
                Assert.False(string.IsNullOrEmpty(item.Summary));
            });
        }

        [Fact]
        public void Info_ShouldReturnFiveItems()
        {
            // Act
            var result = _controller.Info();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
            Assert.All(result, item =>
            {
                Assert.InRange(item.TemperatureC, -20, 55);
                Assert.False(string.IsNullOrEmpty(item.Summary));
            });
        }

        [Fact]
        public void CauseError_ShouldReturnOkResult()
        {
            // Act
            var result = _controller.CauseError();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Maybe crashed, maybe not.", okResult.Value);
        }

        [Fact]
        public void CauseError_ShouldHandleDivideByZeroGracefully()
        {
            // Arrange
            var logger = new LoggerFactory().CreateLogger<WeatherForecastController>();
            var controller = new WeatherForecastController(logger);

            // Act & Assert (Should not throw)
            var exception = Record.Exception(() => controller.CauseError());
            Assert.Null(exception);
        }
    }
}
