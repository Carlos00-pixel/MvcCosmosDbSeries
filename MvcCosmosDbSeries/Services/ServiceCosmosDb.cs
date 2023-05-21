using Microsoft.Azure.Cosmos;
using MvcCosmosDbSeries.Models;

namespace MvcCosmosDbSeries.Services
{
    public class ServiceCosmosDb
    {
        private CosmosClient client;
        public Container containerCosmos;

        public ServiceCosmosDb(CosmosClient client,
            Container container)
        {
            this.client = client;
            this.containerCosmos = container;
        }

        public async Task CreateDatabaseAsync()
        {
            ContainerProperties properties =
                new ContainerProperties("containerseries", "/id");
            await this.client.CreateDatabaseIfNotExistsAsync
                ("seriescosmosdb");
            await this.client.GetDatabase("seriescosmosdb")
                .CreateContainerIfNotExistsAsync(properties);
        }

        public async Task InsertSerieAsync(Serie ser)
        {
            await this.containerCosmos.CreateItemAsync<Serie>
                (ser, new PartitionKey(ser.Id));
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            var query =
                this.containerCosmos.GetItemQueryIterator<Serie>();
            List<Serie> series = new List<Serie>();
            while (query.HasMoreResults)
            {
                var results = await query.ReadNextAsync();
                series.AddRange(results);
            }
            return series;
        }

        public async Task UpdateSerieAsync(Serie ser)
        {
            await this.containerCosmos.UpsertItemAsync<Serie>
                (ser, new PartitionKey(ser.Id));
        }

        public async Task DeleteSerieAsync(string id)
        {
            await this.containerCosmos.DeleteItemAsync<Serie>
                (id, new PartitionKey(id));
        }

        public async Task<Serie> FindSerieAsync(string id)
        {
            ItemResponse<Serie> response =
                await this.containerCosmos.ReadItemAsync<Serie>
                (id, new PartitionKey(id));
            return response.Resource;
        }
    }

}
