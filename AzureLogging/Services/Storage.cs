using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;
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
            
            _cloudTable = tableClient.GetTableReference("datalog");
            _cloudTable.CreateIfNotExists();
        }

        public string SaveBlob(Root response)
        {
            using var stream = new MemoryStream();
            using var streamWriter = new StreamWriter(stream);

            var blobName = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            var fileName = $"ApiResponse{blobName}.json";

            var content = JsonSerializer.Serialize(response);

            streamWriter.Write(content);
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            _blobContainerClient.UploadBlob(fileName, stream);

            return blobName;
        }

        public async Task LogRequest(Root response, string id)
        {
            var serializedResponse = JsonSerializer.Serialize(response);
            var entry = new ApiResponseEntity(id, serializedResponse, DateTime.Now);

            var operation = TableOperation.Insert(entry);
            await _cloudTable.ExecuteAsync(operation);
        }
    }
}
