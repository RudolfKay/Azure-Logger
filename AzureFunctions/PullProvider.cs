using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using AzureLogging.Services;
using System.Linq;
using System;
using AzureLogging.Interfaces;

namespace AzureFunctions
{
    public class PullProvider
    {
        private readonly IBlobService _blobService;
        private readonly ILogService _logService;
        private readonly IPublicApi _publicApi;

        public PullProvider(IPublicApi api, ILogService logService, IBlobService blobService)
        {
            _publicApi = api;
            _logService = logService;
            _blobService = blobService;
        }

        [FunctionName("GetData")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var result = await _publicApi.GetApi();
            var output = result.Entries.First();

            log.LogInformation($"{DateTime.Now}\nAPI: {output.API}\nDescription: {output.Description}");

            var blobName = _blobService.SaveBlob(result);
            await _logService.LogRequest(result, blobName);
        }
    }
}
