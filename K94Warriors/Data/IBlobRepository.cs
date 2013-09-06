using System.IO;
using System.Threading.Tasks;

namespace K94Warriors.Data
{
    public interface IBlobRepository
    {
        Task<T> GetImageAsync<T>(string containerName, string id) where T : Stream, new();
        Task InsertOrUpdateImageAsync(string containerName, string id, Stream stream);
    }
}