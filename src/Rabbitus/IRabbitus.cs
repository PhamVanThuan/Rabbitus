using Rabbitus.Configuration;

namespace Rabbitus
{
	public interface IRabbitus
	{
	    void Publish<TMessage>(TMessage message) where TMessage : class;
	}
}