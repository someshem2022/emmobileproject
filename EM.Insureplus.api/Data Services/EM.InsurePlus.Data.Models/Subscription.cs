using EM.InsurePlus.Data.Models.Identity;

namespace EM.InsurePlus.Data.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}