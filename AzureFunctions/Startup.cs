using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using AzureLogging.Services;
using System;
using Refit;
using AzureLogging.Interfaces;

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
            builder.Services.AddSingleton<IConfig, Config>();
            builder.Services.AddScoped<Storage>();
        }
    }
}
