using Microsoft.AspNetCore.Mvc;
using SimpleProject.Repo;

namespace SimpleProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private IWeatherData repo = new LocalCosmosEmulator("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            IQueryCollection queryParams = this.HttpContext.Request.Query;
            string paramName = "where";
            if(queryParams != null && queryParams.Count > 0 && queryParams.ContainsKey(paramName))
            {
                return await repo.GetDataAync(queryParams[paramName]);
            }
            else
            {
                return await repo.GetDataAync();
            }
        }

        [HttpPost(Name = "PostWeatherForecast")]
        public async Task<WeatherForecast> Post([FromBody] WeatherForecast weather)
        {
            return await repo.AddDataAsync(weather);
        }
    }
}