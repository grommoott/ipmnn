using Newtonsoft.Json;

namespace Serialization
{
    public static class Json
    {
        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject
                (
                    value,
                    Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                );
        }

        public static T Parse<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
