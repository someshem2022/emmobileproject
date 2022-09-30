

namespace EM.InsurePlus.Data.Repositories.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using DO = Models.DataModels;
    using SO = Models.ServiceModels;
    public interface IVehiclePolicyRepository
    {
        Task<bool> Save(DO.VehiclePolicy alert);
        Task<IEnumerable<SO.VehiclePolicy>> Get();
    }
}
