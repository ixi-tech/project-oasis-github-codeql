namespace SimpleProject.Repo
{
    public interface IWeatherData
    {
        public Task<IList<WeatherForecast>> GetDataAync();

        public Task<IList<WeatherForecast>> GetDataAync(string sql);

        public Task<WeatherForecast> AddDataAsync(WeatherForecast forecast);
    }
}
