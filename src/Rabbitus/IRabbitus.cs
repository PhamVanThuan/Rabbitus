using Rabbitus.Consumer;
using Rabbitus.InboundDispatcher;
using Rabbitus.OutboundDispatcher;

namespace Rabbitus
{
	public interface IRabbitus
	{
        IInboundMessageDispatcher InboundDispatcher { get; }
        IOutboundMessageDispatcher OutboundDispatcher { get; }
        IMessageConsumer MessageConsumer { get; }

        void Start();
	    void Publish<TMessage>(TMessage message) where TMessage : class;
	}
}