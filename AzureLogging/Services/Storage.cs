using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;
using AzureLogging.Interfaces;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using AzureLogging.Models;
using System.Text.Json;
using System.Linq;
using System.IO;
using System;

namespace AzureLogging.Services
{
    public class Storage
    {
        private readonly BlobContainerClient _blobContainerClient;
        private readonly CloudTable _cloudTable;
        private readonly IConfig _config;

        public Storage(IConfig config)
        {
            _config = config;

            var client = new BlobServiceClient(_config.ConnectionString);

            try
            {
                _blobContainerClient = client.CreateBlobContainer(_config.BlobContainerName);
            }
            catch
            {
                _blobContainerClient = client.GetBlobContainerClient(_config.BlobContainerName);
            }

            var account = CloudStorageAccount.Parse(_config.ConnectionString);
            var tableClient = account.CreateCloudTableClient(new TableClientConfiguration());
            
            _cloudTable = tableClient.GetTableReference(_config.AzureTableName);
            _cloudTable.CreateIfNotExists();
        }

        public string SaveBlob(Root response)
        {
            using var stream = new MemoryStream();
            using var streamWriter = new StreamWriter(stream);

            var blobName = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            var fileName = $"{_config.FilePrefix}{blobName}.json";

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

        public IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to)
        {
            var items = _cloudTable.ExecuteQuery(new TableQuery<ApiResponseEntity>())
                .Where(i => i.Timestamp >= from && i.Timestamp <= to);

            return items;
        }
        
        public async Task<string> GetBlob(string id)
        {
            var fileName = $"{_config.FilePrefix}{id}.json";
            var blobClient = _blobContainerClient.GetBlobClient(fileName);

            using var stream = new MemoryStream();
            await blobClient.DownloadToAsync(stream);
            stream.Position = 0;

            using var streamReader = new StreamReader(stream);
            var response = await streamReader.ReadToEndAsync();

            return response;
        }
    }
}
