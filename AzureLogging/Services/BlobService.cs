using AzureLogging.Interfaces;
using AzureLogging.Models;
using System.Threading.Tasks;

namespace AzureLogging.Services
{
    public class BlobService : IBlobService
    {
        private readonly IBlobStorage _blobStorage;

        public BlobService(IBlobStorage storage)
        {
            _blobStorage = storage;
        }

        public string SaveBlob(Root response)
        {
            return _blobStorage.SaveBlob(response);
        }

        public Task<string> GetBlob(string id)
        {
            return _blobStorage.GetBlob(id);
        }
    }
}
