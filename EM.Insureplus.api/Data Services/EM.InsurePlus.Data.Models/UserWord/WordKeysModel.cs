using Newtonsoft.Json;

namespace EM.InsurePlus.Data.Models
{
    public class WordKeysModel
    {
        [JsonProperty(PropertyName = "wordId")]
        public string WordId { get; set; } = default!;

        [JsonProperty(PropertyName = "wordPartitionKey")]
        public string WordPartitionKey { get; set; } = default!;

        [JsonProperty(PropertyName = "word")]
        public string Word { get; set; } = default!;

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}