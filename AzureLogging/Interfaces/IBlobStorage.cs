using System.Threading.Tasks;
using AzureLogging.Models;

namespace AzureLogging.Interfaces
{
    public interface IBlobStorage
    {
        public string SaveBlob(Root response);

        public Task<string> GetBlob(string id);
    }
}
