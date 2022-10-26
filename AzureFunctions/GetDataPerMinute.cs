using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using AzureLogging.Services;
using System.Linq;
using System;

namespace AzureFunctions
{
    public class GetDataPerMinute
    {
        private readonly Storage _blobStorage;
        private readonly IPublicApi _publicApi;

        public GetDataPerMinute(IPublicApi api)
        {
            _publicApi = api;
            _blobStorage = new Storage();
        }

        [FunctionName("GetData")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var result = await _publicApi.GetApi();
            var output = result.Entries.First();

            log.LogInformation($"{DateTime.Now}\nAPI: {output.API}\nDescription: {output.Description}");

            var blobName = _blobStorage.SaveBlob(result);
            await _blobStorage.LogRequest(result, blobName);
        }
    }
}
