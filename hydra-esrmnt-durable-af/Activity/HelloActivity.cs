using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace Hydra.Esrmnt.Durable.AF.Activity
{
    public static class HelloActivity
    {
        [FunctionName("ActivityFunction")]
        public static string SayHelloActivity(
            [ActivityTrigger] string name,
            ILogger log)
        {
            log.LogInformation($"Saying hello to {name}.");
            return $"Hello {name}!";
        }
    }
}
