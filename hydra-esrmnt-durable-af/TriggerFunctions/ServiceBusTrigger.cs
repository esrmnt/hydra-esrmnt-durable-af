using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace Hydra.Esrmnt.Durable.AF.TriggerFunctions
{
    public static class ServiceBusTrigger
    {
        [FunctionName(nameof(ServiceBusTrigger))]
        public static async Task ServiceBusStart(
        [ServiceBusTrigger(queueName:"hydra-inbound-notifications", Connection = "ServiceBusConnection")]
        ServiceBusReceivedMessage messages,
        ServiceBusMessageActions messageActions,
        [DurableClient] IDurableOrchestrationClient starter,
        ILogger log)
        {
            var instanceId = await starter.StartNewAsync("OrchestratorFunction", null);
        }
    }
}
