using System.Collections.Generic;
using System.Threading.Tasks;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public interface ICosmosService
    {
        Task<List<FileEntity>> GetItemsAsync(string query);
        Task AddItemAsync(FileEntity file);
        Task UpdateItemAsync(FileEntity file);
        Task DeleteItemAsync(FileEntity file);
    }
}