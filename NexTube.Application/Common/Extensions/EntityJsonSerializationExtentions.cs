using Newtonsoft.Json;
using NexTube.Domain.Entities.Abstract;

namespace NexTube.Application.Common.Extensions {
    public static class EntityJsonSerializationExtentions {
        //private static readonly JsonSerializer serializer = new JsonSerializer();
        public static string ToJson(this object entity) {
            return JsonConvert.SerializeObject(entity);
        }
        public static DestinationType FromJson<DestinationType>(this string serialized) {
            return JsonConvert.DeserializeObject<DestinationType>(serialized)
                ?? throw new JsonException($"{nameof(serialized)} could not be deserialized");
        }
    }
}
