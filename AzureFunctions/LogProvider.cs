using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AzureLogging.Interfaces;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using System;

namespace AzureFunctions
{
    public class LogProvider
    {
        private readonly ILogService _logService;

        public LogProvider(ILogService logService)
        {
            _logService = logService;
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

            var items = _logService.GetLogs(fromDateTime, toDateTime);

            return await Task.FromResult<IActionResult>(new OkObjectResult(items));
        }
    }
}
