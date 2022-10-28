using System.Collections.Generic;
using System.Threading.Tasks;
using AzureLogging.Models;
using System;

namespace AzureLogging.Interfaces
{
    public interface ILogService
    {
        public Task LogRequest(Root response, string id);

        public IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to);
    }
}
