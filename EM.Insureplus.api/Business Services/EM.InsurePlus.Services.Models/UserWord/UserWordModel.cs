namespace EM.InsurePlus.Services.Models
{
    public class UserWordModel
    {
        public string Id { get; set; } = default!;
        public string PartitionKey { get; set; } = default!;
        public int UserId { get; set; }
        public List<WordKeysModel>? Words { get; set; }
    }
}