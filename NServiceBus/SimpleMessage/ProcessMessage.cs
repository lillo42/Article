using NServiceBus;

namespace SimpleMessage
{
    public class ProcessMessage : IMessage 
    {
        public string Value { get; set; }
    }
}