using Newtonsoft.Json;

namespace EM.InsurePlus.Data.Models
{
    public class UserWordModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = default!;

        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; } = default!;

        [JsonProperty(PropertyName = "userId")]
        public int UserId { get; set; }

        [JsonProperty(PropertyName = "words")]
        public List<WordKeysModel>? Words { get; set; }
    }
}