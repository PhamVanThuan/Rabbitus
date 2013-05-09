using Rabbitus.Actors;

namespace Rabbitus
{
	public interface IRabbitus
	{
        void Start();
	    void Subscribe<TActor>() where TActor : Actor<TActor>;
	    void Publish<TMessage>(TMessage message) where TMessage : class;
	}
}