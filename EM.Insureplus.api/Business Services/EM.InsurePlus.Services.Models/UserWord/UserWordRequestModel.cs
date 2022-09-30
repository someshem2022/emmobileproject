namespace EM.InsurePlus.Services.Models
{
    using EM.InsurePlus.Common.Models;

    public class GetUserWordRequestModel
    {
        public int UserId { get; set; }
        public Paging Page { get; set; }
    }
}