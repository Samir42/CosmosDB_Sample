using CosmosDB_Sample.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserModel = CosmosDB_Sample.Models.User;

namespace CosmosDB_Sample.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;
        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddUserAsync(UserModel user)
        {
            await _container.CreateItemAsync<UserModel>(user, new PartitionKey(user.Id));
        }

        public async Task DeleteUserAsync(string id)
        {
            await _container.DeleteItemAsync<UserModel>(id, new PartitionKey(id));
        }

        public async Task<UserModel> GetUserAsync(string id)
        {
            try
            {
                ItemResponse<UserModel> response = await _container.ReadItemAsync<UserModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<UserModel>(new QueryDefinition(queryString));
            List<UserModel> results = new List<UserModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateUserAsync(string id, UserModel user)
        {
            await _container.UpsertItemAsync<UserModel>(user, new PartitionKey(id));
        }
    }
}
