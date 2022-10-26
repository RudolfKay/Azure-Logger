using System;

namespace AzureLogging.Interfaces
{
    public interface IConfig
    {
        public string ConnectionString => Environment.GetEnvironmentVariable("UseDevelopmentStorage=true");
        public string BlobContainerName => Environment.GetEnvironmentVariable("azure-logging-blob");
        public string AzureTableName => Environment.GetEnvironmentVariable("datalog");
        public string FilePrefix => Environment.GetEnvironmentVariable("ApiResponse-");
    }
}
