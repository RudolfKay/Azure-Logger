using AzureLogging.Interfaces;
using System;

namespace AzureFunctions
{
    public class Config : IConfig
    {
        public string ConnectionString => Environment.GetEnvironmentVariable("UseDevelopmentStorage=true");
        public string BlobContainerName => Environment.GetEnvironmentVariable("azure-logging-blob");
        public string AzureTableName => Environment.GetEnvironmentVariable("datalog");
        public string FilePrefix => Environment.GetEnvironmentVariable("ApiResponse-");
    }
}
