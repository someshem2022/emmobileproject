using EM.InsurePlus.Data.Contract;
using EM.InsurePlus.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace EM.InsurePlus.Data.Models.Identity
{
    public class User : IdentityUser<int>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActivated { get; set; }       
        
        
    }
}