using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.InsurePlus.Repository.Contract
{
    using SO = EM.InsurePlus.Services.Models;
    public interface IVehiclePolicyRepository
    {
        Task<bool> Create(SO.VehiclePolicy model);
    }
}
