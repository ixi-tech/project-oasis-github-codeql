using Newtonsoft.Json;

namespace SimpleProject
{
    public class WeatherForecast
    {
        [JsonProperty("id")]
        public string? id { get; set; }

        [JsonProperty("partition_key")]
        public string? parition_key { get; set; }

        [JsonProperty("date")]
        public DateOnly Date { get; set; }

        [JsonProperty("temp_celsius")]
        public int TemperatureC { get; set; }

        [JsonProperty("summary")]
        public string? Summary { get; set; }
    }
}