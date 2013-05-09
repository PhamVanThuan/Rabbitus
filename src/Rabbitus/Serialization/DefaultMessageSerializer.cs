using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Rabbitus.Serialization
{
    public class DefaultMessageSerializer : IMessageSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.None,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public string ContentType
        {
            get { return "application/json"; }
        }

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