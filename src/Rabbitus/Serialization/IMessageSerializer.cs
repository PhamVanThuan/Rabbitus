namespace Rabbitus.Serialization
{
    public interface IMessageSerializer
    {
        string SerializeMessage(object message);
        object DeserializeMessage(string data);
    }
}