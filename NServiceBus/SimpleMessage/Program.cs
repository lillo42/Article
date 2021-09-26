using System;
using System.Threading.Tasks;
using NServiceBus;

namespace SimpleMessage
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var endpointConfiguration = new EndpointConfiguration("SimpleMessage");

            endpointConfiguration.UseTransport<LearningTransport>();
            
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine();
            Console.WriteLine("Press 'Enter' to send a ProcessMessage message");
            Console.WriteLine("Press any other key to exit");
            while (true)
            {
                Console.WriteLine();
                if (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    break;
                }
                var val = Guid.NewGuid();
                var startOrder = new ProcessMessage
                {
                    Value = val.ToString()
                };
                await endpointInstance.SendLocal(startOrder);
                Console.WriteLine($"Sent ProcessMessage with OrderId {val}.");
            }

            await endpointInstance.Stop();
        }
    }
}
