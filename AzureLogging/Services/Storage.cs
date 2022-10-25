using Azure.Storage.Blobs;
using Microsoft.VisualBasic;
using System.Security.Cryptography;

namespace AzureLogging.Services
{
    public class Storage
    {
        private readonly BlobContainerClient _blobContainerClient;

        public Storage()
        {
            var client = new BlobServiceClient("UseDevelopmentStorage=true");

            try
            {
                _blobContainerClient = client.CreateBlobContainer("AzureLoggingBlob");
            }
            catch
            {
                _blobContainerClient = client.GetBlobContainerClient("AzureLoggingBlob");
            }
        }
    }
}
