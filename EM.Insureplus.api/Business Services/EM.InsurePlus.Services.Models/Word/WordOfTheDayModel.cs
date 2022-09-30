namespace EM.InsurePlus.Services.Models
{
    public class WordOfTheDayModel
    {
        public string Id { get; set; }
        public string PartitionKey { get; set; }
        public string Word { get; set; }
        public string Pronunciation { get; set; }
        public string Meaning { get; set; }
        public string[] Modes { get; set; }
        public IEnumerable<NounModel> NounList { get; set; }
        public IEnumerable<VerbModel> VerbList { get; set; }
        public IEnumerable<AdjectiveModel> AdjectiveList { get; set; }
        public IEnumerable<ImageModel> ImageList { get; set; }
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