using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace Hydra.Esrmnt.Durable.AF.Orchestrators
{
    public static class Orchestrator
    {
        [FunctionName("OrchestratorFunction")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>
            {
                // Replace "hello" with the name of your Durable Activity Function.
                await context.CallActivityAsync<string>("ActivityFunction", "Tokyo"),
                await context.CallActivityAsync<string>("ActivityFunction", "Seattle"),
                await context.CallActivityAsync<string>("ActivityFunction", "London")
            };

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }
    }
}
