using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(Hydra.Esrmnt.Durable.AF.Startup))]
namespace Hydra.Esrmnt.Durable.AF
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configBuilder = new ConfigurationBuilder()
                .AddConfiguration(builder.Services.BuildServiceProvider().GetService<IConfiguration>())
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddHttpClient();
        }
    }
}