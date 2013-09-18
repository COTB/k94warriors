using System.IO;
using System.Threading.Tasks;

namespace K94Warriors.Data.Contracts
{
    public interface IBlobRepository
    {
        Task<T> GetImageAsync<T>(string id) where T : Stream, new();
        Task InsertOrUpdateImageAsync(string id, Stream stream);
        Task<bool> DeleteImageIfExistsAsync(string id);
    }
}