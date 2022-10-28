using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using AzureLogging.Services;
using AzureLogging.Interfaces;

namespace AzureFunctions
{
    public class BlobProvider
    {
        private readonly IBlobService _blobService;

        public BlobProvider(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [FunctionName("BlobProvider")]
        public async Task<IActionResult> Run(
            [HttpTrigger("get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetBlob HTTP trigger function processed a request.");

            string id = req.Query["id"];
            var result = await _blobService.GetBlob(id);

            return await Task.FromResult<IActionResult>(new OkObjectResult(result));
        }
    }
}
