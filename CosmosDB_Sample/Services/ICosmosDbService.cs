using CosmosDB_Sample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosDB_Sample.Services
{
    public interface ICosmosDbService
    {
        Task AddUserAsync(User user);

        Task DeleteUserAsync(string id);

        Task<User> GetUserAsync(string id);

        Task<IEnumerable<User>> GetUsersAsync(string queryString);

        Task UpdateUserAsync(string id, User user);
    }
}
