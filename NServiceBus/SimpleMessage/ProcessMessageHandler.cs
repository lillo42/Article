using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace SimpleMessage
{
    public class ProcessMessageHandler  : IHandleMessages<ProcessMessage>
    {
        // NServiceBus also provider the logger
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProcessMessageHandler));

        public Task Handle(ProcessMessage message, IMessageHandlerContext context)    
        {
            Log.InfoFormat("Received message with value: {0}", message.Value);
            return Task.CompletedTask;
        }
    }
}