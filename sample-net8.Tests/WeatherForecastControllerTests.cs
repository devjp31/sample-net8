using Microsoft.Extensions.Logging;
using sample_net8.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // Arrange & Act
            var result = _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public void Get_ShouldContainValidTemperatureRange()
        {
            // Arrange
            var result = _controller.Get();

            // Assert
            Assert.All(result, item =>
            {
                Assert.InRange(item.TemperatureC, -20, 55);
                Assert.False(string.IsNullOrEmpty(item.Summary));
            });
        }
    }

}
