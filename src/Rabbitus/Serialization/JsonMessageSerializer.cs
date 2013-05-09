using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Rabbitus.Serialization
{
    public class JsonMessageSerializer : IMessageSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public string SerializeMessage(object message)
        {
            return JsonConvert.SerializeObject(message, Settings);
        }

        public object DeserializeMessage(string message)
        {
            return JsonConvert.DeserializeObject(message, Settings);
        }
    }
}