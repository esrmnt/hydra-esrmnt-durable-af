using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace hydra_esrmnt_durable_af
{
    public static class DurableOrchestrator
    {
        [FunctionName("OrchestratorFunction")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context
            )
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

        [FunctionName("ActivityFunction")]
        public static string SayHelloActivity(
            [ActivityTrigger] string name, 
            ILogger log
            )
        {
            log.LogInformation($"Saying hello to {name}.");
            return $"Hello {name}!";
        }

        [FunctionName("OrchestratorClient")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log
            )
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("OrchestratorFunction", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}