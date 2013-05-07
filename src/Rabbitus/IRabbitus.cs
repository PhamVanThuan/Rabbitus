using Rabbitus.Configuration;

namespace Rabbitus
{
	public interface IRabbitus
	{
        RabbitusConfiguration Configuration { get; }
	    void Publish<TMessage>(TMessage message) where TMessage : class;
	}
}