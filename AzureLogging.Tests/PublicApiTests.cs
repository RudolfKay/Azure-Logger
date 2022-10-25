using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AzureLogging.Services;
using FluentAssertions;
using Refit;

namespace AzureLogging.Tests
{
    [TestClass]
    public class PublicApiTests
    {
        [TestMethod]
        public async Task TestMethod1Async()
        {
            var publicApi = RestService.For<IPublicApi>("https://api.publicapis.org");
            var octocat = await publicApi.GetApi();

            octocat.Should().NotBeNull();
        }
    }
}
