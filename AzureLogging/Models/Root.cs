using System.Collections.Generic;

namespace AzureLogging.Models
{
    public class Root
    {
        public int Count { get; set; }
        public List<ApiResponse> Entries { get; set; }
    }
}
