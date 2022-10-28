using System.Threading.Tasks;
using AzureLogging.Models;

namespace AzureLogging.Interfaces
{
    public interface IBlobService
    {
        public string SaveBlob(Root response);

        public Task<string> GetBlob(string id);
    }
}
