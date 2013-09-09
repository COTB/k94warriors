using System;
using System.IO;
using System.Threading.Tasks;
using K94Warriors.Core.AsyncExtensions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace K94Warriors.Data
{
    public class BlockBlobRepository : IBlobRepository
    {
        private readonly CloudStorageAccount _account;
        private readonly CloudBlobClient _client;

        public BlockBlobRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");
            _account = CloudStorageAccount.Parse(connectionString);
            _client = _account.CreateCloudBlobClient();
        }

        public async Task<T> GetImageAsync<T>(string containerName, string id)
            where T : Stream, new()
        {
            var container = _client.GetContainerReference(containerName);
            var blockBlob = container.GetBlockBlobReference(id);
            var memoryStream = new T();

            return await blockBlob.DownloadToStreamAsync(memoryStream);
        }

        public async Task InsertOrUpdateImageAsync(string containerName, string id, Stream stream)
        {
            var container = _client.GetContainerReference(containerName);
            var blockBlob = container.GetBlockBlobReference(id);
            await blockBlob.UploadFromStreamAsync(stream);
        }
    }
}