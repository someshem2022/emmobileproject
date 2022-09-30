namespace EM.InsurePlus.Services.Models
{
    using EM.InsurePlus.Services.Models.Enums;

    public class WordOfTheDayRequestModel
    {
        public int UserId { get; set; }
        public bool IsSkipWord { get; set; }
        public WordRequestModes Mode { get; set; }
        public SubscriptionModes SubscriptionPlan { get; set; }
        public List<WordKeysModel> WordIdList { get; set; }
    }
}