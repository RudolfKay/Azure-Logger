using Azure.Storage.Blobs;
using AzureLogging.Models;
using System.Text.Json;
using System.IO;
using System;

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

        public void SaveBlob(ApiResponse response)
        {
            using var stream = new MemoryStream();
            using var streamWriter = new StreamWriter(stream);

            var fileName = $"ApiResponse-{DateTime.Now}.json";
            var content = JsonSerializer.Serialize(response);

            streamWriter.Write(content);
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            _blobContainerClient.UploadBlob(fileName, stream);
        }
    }
}
