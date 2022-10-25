using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using AzureLogging.Services;
using System;
using Refit;

[assembly: FunctionsStartup(typeof(AzureFunctions.Startup))]

namespace AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddRefitClient<IPublicApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.publicapis.org"));
        }
    }
}
