using AzureLogging.Interfaces;
using AzureLogging.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace AzureLogging.Services
{
    public class LogService : ILogService
    {
        private readonly ILogStorage _logStorage;

        public LogService(ILogStorage storage)
        {
            _logStorage = storage;
        }

        public Task LogRequest(Root response, string id)
        {
            return _logStorage.LogRequest(response, id);
        }

        public IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to)
        {
            return _logStorage.GetLogs(from, to);
        }
    }
}
