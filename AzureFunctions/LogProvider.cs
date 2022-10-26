using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using AzureLogging.Services;
using System;

namespace AzureFunctions
{
    public class LogProvider
    {
        private readonly Storage _storage;

        public LogProvider(Storage storage)
        {
            _storage = storage;
        }

        [FunctionName("LogProvider")]
        public async Task<IActionResult> Run(
            [HttpTrigger("get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetLogs HTTP trigger function processed a request.");

            string from = req.Query["from"];
            string to = req.Query["to"];

            var fromDateTime = DateTime.Parse(from);
            var toDateTime = DateTime.Parse(to);

            var items = _storage.GetLogs(fromDateTime, toDateTime);

            return await Task.FromResult<IActionResult>(new OkObjectResult(items));
        }
    }
}
