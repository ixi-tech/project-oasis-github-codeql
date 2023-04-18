using Microsoft.Azure.Cosmos;
using System.Text;

namespace SimpleProject.Repo
{
    public class LocalCosmosEmulator : IWeatherData
    {
        private string dbHost;
        private string dbPassw;
        private CosmosClient dbClient = null;

        public LocalCosmosEmulator(string dbHost, string dbPassw)
        {
            this.dbHost = dbHost;
            this.dbPassw = dbPassw;
        }


        public async Task<IList<WeatherForecast>> GetDataAync()
        {
            QueryDefinition finalQuery = new QueryDefinition("select value c from c ");
            var container = await GetContainer();
            IList<WeatherForecast> list = new List<WeatherForecast>();

            FeedIterator<WeatherForecast> feedIterator = container.GetItemQueryIterator<WeatherForecast>(finalQuery);

            while (feedIterator.HasMoreResults)
            {
                FeedResponse<WeatherForecast> response = await feedIterator.ReadNextAsync().ConfigureAwait(false);
                foreach (WeatherForecast item in response)
                {
                    list.Add(item);
                }
            }

            return list;
        }

        public async Task<IList<WeatherForecast>> GetDataAync(string sql)
        {
            QueryDefinition finalQuery = new QueryDefinition(sql);
            var container = await GetContainer();
            IList<WeatherForecast> list = new List<WeatherForecast>();

            FeedIterator<WeatherForecast> feedIterator = container.GetItemQueryIterator<WeatherForecast>(finalQuery);

            while (feedIterator.HasMoreResults)
            {
                FeedResponse<WeatherForecast> response = await feedIterator.ReadNextAsync().ConfigureAwait(false);
                foreach (WeatherForecast item in response)
                {
                    list.Add(item);
                }
            }

            return list;
        }

        public async Task<WeatherForecast> AddDataAsync(WeatherForecast forecast)
        {
            var container = await GetContainer();
            forecast.id = Guid.NewGuid().ToString();
            forecast.parition_key = forecast.id.Substring(0, 2);
            return await container.UpsertItemAsync<WeatherForecast>(forecast, new PartitionKey(forecast.parition_key));
        }

        private void InitCosmosClient()
        {
            if(this.dbClient != null)
            {
                return;
            }

            this.dbClient = new CosmosClient(this.dbHost, this.dbPassw);
        }

        private async Task<Container> GetContainer()
        {
            this.InitCosmosClient();
            var db = await this.dbClient.CreateDatabaseIfNotExistsAsync("db-weather");
            var containerProperties = new ContainerProperties()
            {
                Id="id",
                PartitionKeyPath="/partition_key",
            };
            var containerResp = await db.Database.CreateContainerIfNotExistsAsync(containerProperties, 400);
            return containerResp.Container;
        }
    }
}
