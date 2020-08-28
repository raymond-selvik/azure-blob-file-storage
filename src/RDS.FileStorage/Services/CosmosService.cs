using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public class CosmosService : ICosmosService
    {
        private Container container;

        public CosmosService(CosmosClient client, string databaseName, string containerName)
        {
            this.container = client.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(FileEntity file)
        {
            await this.container.CreateItemAsync(file);
        }

        public async Task DeleteItemAsync(FileEntity file)
        {
         
            await this.container.DeleteItemAsync<FileEntity>(file.Id, new PartitionKey(file.Drive));
        }

        public async Task<List<FileEntity>> GetItemsAsync(string queryString)
        {
            var query = this.container.GetItemQueryIterator<FileEntity>(new QueryDefinition(queryString));

            List<FileEntity> results = new List<FileEntity>();

            while(query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(FileEntity file)
        {
            await this.container.UpsertItemAsync<FileEntity>(file, new PartitionKey(file.Drive));
        }
    }
}