using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace sample_net8.Tests
{
	public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
	{
		private readonly WebApplicationFactory<Program> _factory;

		public ProgramTests(WebApplicationFactory<Program> factory)
		{
			_factory = factory;
		}

		[Fact]
		public async Task Get_WeatherForecast_Endpoint_ReturnsSuccess()
		{
			// Arrange
			var client = _factory.CreateClient();

			// Act
			var response = await client.GetAsync("/WeatherForecast");

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async Task Get_Info_Endpoint_ReturnsSuccess()
		{
			// Arrange
			var client = _factory.CreateClient();

			// Act
			var response = await client.GetAsync("/WeatherForecast/Info");

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async Task Get_Crash_Endpoint_ReturnsOkMessage()
		{
			// Arrange
			var client = _factory.CreateClient();

			// Act
			var response = await client.GetAsync("/WeatherForecast/crash");

			// Assert
			response.EnsureSuccessStatusCode();
			var message = await response.Content.ReadAsStringAsync();
			Assert.Contains("Maybe crashed", message);
		}
	}
}
