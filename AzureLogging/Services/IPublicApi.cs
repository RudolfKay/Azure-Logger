using System.Threading.Tasks;
using AzureLogging.Models;
using Refit;

namespace AzureLogging.Services
{
    public interface IPublicApi
    {
        [Get("/random?auth=null")]
        Task<Root> GetApi();
    }
}
