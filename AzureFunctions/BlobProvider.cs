using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using AzureLogging.Services;

namespace AzureFunctions
{
    public class BlobProvider
    {
        private readonly Storage _storage;

        public BlobProvider(Storage storage)
        {
            _storage = storage;
        }

        [FunctionName("BlobProvider")]
        public async Task<IActionResult> Run(
            [HttpTrigger("get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetBlob HTTP trigger function processed a request.");

            string id = req.Query["id"];
            var result = await _storage.GetBlob(id);

            return await Task.FromResult<IActionResult>(new OkObjectResult(result));
        }
    }
}
