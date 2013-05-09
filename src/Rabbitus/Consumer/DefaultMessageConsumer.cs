using Rabbitus.InboundDispatcher;

namespace Rabbitus.Consumer
{
    public class DefaultMessageConsumer : IMessageConsumer
    {
        private readonly IInboundMessageDispatcher _inboundDispatcher;

        public DefaultMessageConsumer(IInboundMessageDispatcher inboundDispatcher)
        {
            _inboundDispatcher = inboundDispatcher;
        }

        public void Start()
        {
            // Start consume thread
        }

        public void Stop()
        {
            // Wait and stop consume thread
        }
    }
}