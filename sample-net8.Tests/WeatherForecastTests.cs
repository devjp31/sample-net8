using sample_net8;
using Xunit;

namespace sample_net8.Tests
{
    public class WeatherForecastTests
    {
        [Fact]
        public void TemperatureF_ShouldReturnCorrectValue()
        {
            // Arrange
            var forecast = new WeatherForecast { TemperatureC = 0 };

            // Act
            var result = forecast.TemperatureF;

            // Assert
            Assert.Equal(32, result);
        }

        [Fact]
        public void Summary_ShouldAllowSettingAndGettingValue()
        {
            // Arrange
            var forecast = new WeatherForecast();

            // Act
            forecast.Summary = "Hot";

            // Assert
            Assert.Equal("Hot", forecast.Summary);
        }

        [Fact]
        public void Date_ShouldAllowSettingAndGettingValue()
        {
            // Arrange
            var today = DateOnly.FromDateTime(DateTime.Now);
            var forecast = new WeatherForecast { Date = today };

            // Assert
            Assert.Equal(today, forecast.Date);
        }
    }
}
