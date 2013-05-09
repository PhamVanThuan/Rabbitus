using Newtonsoft.Json;

namespace Rabbitus.Serialization
{
    public class JsonMessageSerializer : IMessageSerializer
    {
        private static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public string SerializeMessage(object message)
        {
            return JsonConvert.SerializeObject(message, Settings);
        }

        public object DeserializeMessage(string message)
        {
            return JsonConvert.DeserializeObject(message);
        }
    }
}