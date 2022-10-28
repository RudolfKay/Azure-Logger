using AzureLogging.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace AzureLogging.Interfaces
{
    public interface IStorage
    {
        public string SaveBlob(Root response);

        public Task<string> GetBlob(string id);

        public Task LogRequest(Root response, string id);

        public IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to);
    }
}
