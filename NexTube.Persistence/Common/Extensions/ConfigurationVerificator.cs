using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace NexTube.Persistence.Services {
    internal record struct ConfigurationSetting {
        public string Path { get; init; }
        public Func<string> DefaultValueGenerator { get; set; }
    }
    internal static class ConfigurationVerificator {
        internal static int EnsureSettingsExist(IConfiguration configuration, List<ConfigurationSetting> settings, string configurationFile) {
            string json = File.ReadAllText(configurationFile);
            JsonObject fileRoot = (JsonObject?)JsonNode.Parse(json) ?? throw new JsonException();

            foreach (var setting in settings) {
                if (configuration[setting.Path] is not null)
                    continue;
                
                var value = setting.DefaultValueGenerator.Invoke();
                configuration[setting.Path] = value;
                SetValueByPath(fileRoot, setting.Path, value);
            }

            string updatedJson = fileRoot.ToString();
            File.WriteAllText(configurationFile, updatedJson);
            
            return 0;
        }

        private static void SetValueByPath(JsonObject jsonObject, string path, string newValue) {
            string[] segments = path.Split(':');
            JsonObject? currentObject = jsonObject;

            for (int i = 0; i < segments.Length; i++) {
                string segment = segments[i];
                if (currentObject is null)
                    continue;

                if (currentObject.TryGetPropertyValue(segment, out var node)) {
                    if (i == segments.Length - 1) {
                        // If it's the last segment, update the value
                        if (node is JsonValue) {
                            // replace with value update
                            //value.Value = newValue;
                        }
                        else {
                            // The last segment should be a value, not an object or array
                            throw new InvalidOperationException("The last segment should be a value.");
                        }
                    }
                    else if (node is JsonObject nestedObject) {
                        // Continue to the next object
                        currentObject = nestedObject;
                    }
                    else if (node is JsonArray jsonArray) {
                        if (i == segments.Length - 1) {
                            // If it's the last segment and an array, create a new object as a value
                            var newObject = new JsonObject();
                            jsonArray.Add(newObject);
                            currentObject = newObject;
                        }
                        else {
                            // For array elements, create an object if it doesn't exist
                            if (jsonArray.Count == 0) {
                                jsonArray.Add(new JsonObject());
                            }
                            currentObject = jsonArray[jsonArray.Count - 1] as JsonObject;
                        }
                    }
                }
                else {
                    if (i == segments.Length - 1) {
                        // If it's the last segment, create a new value
                        currentObject[segment] = JsonValue.Create(newValue);
                    }
                    else {
                        // Create a new object for the next segment
                        var newObject = new JsonObject();
                        currentObject[segment] = newObject;
                        currentObject = newObject;
                    }
                }
            }
        }
    }
}
