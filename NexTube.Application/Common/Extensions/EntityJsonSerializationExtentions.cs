using Newtonsoft.Json;

namespace NexTube.Application.Common.Extensions {
    public static class EntityJsonSerializationExtentions {
        public static string ToJson(this object entity) {
            return JsonConvert.SerializeObject(entity);
        }
        public static DestinationType FromJson<DestinationType>(this string serialized) {
            return JsonConvert.DeserializeObject<DestinationType>(serialized)
                ?? throw new JsonException($"{nameof(serialized)} could not be deserialized");
        }
    }
}
