namespace EM.InsurePlus.Services.Models
{
    public class WordKeysModel
    {
        public string WordId { get; set; } = default!;
        public string WordPartitionKey { get; set; } = default!;
        public string Word { get; set; } = default!;
        public DateTime Date { get; set; }
    }
}