using Microsoft.AspNetCore.Mvc;

namespace sample_net8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger; 
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        // BLOCKER: Hardcoded password + SQL Injection risk
        [HttpGet("login")]
        public IActionResult InsecureLogin(string username)
        {
            string password = "admin123"; // Hardcoded password (Security Hotspot / Blocker)
            string connStr = "Server=localhost;Database=DemoDB;User Id=sa;Password=" + password + ";";
            using var conn = new SqlConnection(connStr);

            // SQL Injection risk (Concatenated SQL)
            string query = "SELECT * FROM Users WHERE username = '" + username + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            var reader = cmd.ExecuteReader();
            return Ok("Login attempted");
        }

        // MAJOR: Unhandled exception + poor logging practice
        [HttpGet("crash")]
        public IActionResult CauseError()
        {
            try
            {
                int x = 0;
                int y = 10 / x; // Divide by zero (Unhandled potential exception)
            }
            catch
            {
                // Swallowing exception — poor error handling
            }
            return Ok("Maybe crashed, maybe not.");
        }

        // MINOR: Dead code + redundant allocation
        [HttpGet("redundant")]
        public IActionResult RedundantCode()
        {
            string temp = "This is unnecessary"; // Unused variable
            var numbers = new List<int> { 1, 2, 3 };
            foreach (var n in numbers)
            {
                var doubled = n * 2; // Unused calculation (code smell)
            }
            return Ok("Redundant logic executed.");
        }
    }
}
