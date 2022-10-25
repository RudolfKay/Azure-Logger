using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using AzureLogging.Services;
using System.Linq;
using System;

namespace AzureFunctions
{
    public class GetDataPerMinute
    {
        private IPublicApi _publicApi;

        public GetDataPerMinute(IPublicApi api)
        {
            _publicApi = api;
        }

        [FunctionName("GetData")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var result = await _publicApi.GetApi();
            var output = result.entries.First();

            log.LogInformation($"{DateTime.Now}\nAPI: {output.API}\nDescription: {output.Description}");
        }
    }
}
