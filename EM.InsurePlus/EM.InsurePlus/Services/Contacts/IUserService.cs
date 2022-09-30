using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EM.InsurePlus.Models;

namespace EM.InsurePlus.Services.Contacts
{
    public interface IUserService
    {
        Task<bool> AuthToken(AuthorizeModel model);

        Task<bool> Register(User model);
    }
}
