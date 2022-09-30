using EM.InsurePlus.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EM.InsurePlus.Services.Contacts
{
    public interface IPolicyService
    {
        Task<VehiclePolicy> CreatePolicy(VehiclePolicy model);
    }
}
