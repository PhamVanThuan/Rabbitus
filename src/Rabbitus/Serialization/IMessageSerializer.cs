namespace Rabbitus.Serialization
{
    public interface IMessageSerializer
    {
        string ContentType { get; }
        string SerializeMessage(object message);
        object DeserializeMessage(string data);
    }
}