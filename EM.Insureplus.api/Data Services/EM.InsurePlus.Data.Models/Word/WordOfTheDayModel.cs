using Newtonsoft.Json;

namespace EM.InsurePlus.Data.Models
{
    public class WordOfTheDayModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }

        [JsonProperty(PropertyName = "word")]
        public string Word { get; set; }

        [JsonProperty(PropertyName = "pronunciation")]
        public string Pronunciation { get; set; }

        [JsonProperty(PropertyName = "meaning")]
        public string Meaning { get; set; }

        [JsonProperty(PropertyName = "modes")]
        public string[] Modes { get; set; }

        [JsonProperty(PropertyName = "nounList")]
        public IEnumerable<NounModel> NounList { get; set; }

        [JsonProperty(PropertyName = "verbList")]
        public IEnumerable<VerbModel> VerbList { get; set; }

        [JsonProperty(PropertyName = "adjectiveList")]
        public IEnumerable<AdjectiveModel> AdjectiveList { get; set; }

        [JsonProperty(PropertyName = "imageList")]
        public IEnumerable<ImageModel> ImageList { get; set; }

        [JsonProperty(PropertyName = "videoList")]
        public IEnumerable<VideoModel> VideoList { get; set; }

        public WordOfTheDayModel()
        {
            Id = string.Empty;
            PartitionKey = string.Empty;
            Word = string.Empty;
            Pronunciation = string.Empty;
            Meaning = string.Empty;
            NounList = new List<NounModel>();
            VerbList = new List<VerbModel>();
            AdjectiveList = new List<AdjectiveModel>();
            ImageList = new List<ImageModel>();
            VideoList = new List<VideoModel>();
        }
    }
}