using EM.InsurePlus.Services.Models.Enums;

namespace EM.InsurePlus.Services.Models
{
    public class SubscriptionModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}