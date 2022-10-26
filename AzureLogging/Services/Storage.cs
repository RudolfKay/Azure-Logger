using Microsoft.Azure.Cosmos.Table;
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
        private readonly CloudTable _cloudTable;

        public Storage()
        {
            var client = new BlobServiceClient("UseDevelopmentStorage=true");

            try
            {
                _blobContainerClient = client.CreateBlobContainer("azure-logging-blob");
            }
            catch
            {
                _blobContainerClient = client.GetBlobContainerClient("azure-logging-blob");
            }

            var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            var tableClient = account.CreateCloudTableClient(new TableClientConfiguration());
            
            _cloudTable = tableClient.GetTableReference("logging-data");
            _cloudTable.CreateIfNotExists();
        }

        public void SaveBlob(Root response)
        {
            using var stream = new MemoryStream();
            using var streamWriter = new StreamWriter(stream);

            var fileName = $"ApiResponse{DateTime.Now:HHmmss}.json";
            var content = JsonSerializer.Serialize(response);

            streamWriter.Write(content);
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            _blobContainerClient.UploadBlob(fileName, stream);
        }
    }
}
