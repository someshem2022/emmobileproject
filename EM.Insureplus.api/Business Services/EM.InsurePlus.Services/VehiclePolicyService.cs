using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.InsurePlus.Repository.Contract;
using EM.InsurePlus.Services.Contract;
using SO = EM.InsurePlus.Services.Models;
using DO = EM.InsurePlus.Data.Models;
namespace EM.InsurePlus.Services
{
    public class VehiclePolicyService : IVehiclePolicyService
    {
        private readonly IVehiclePolicyRepository vehiclePolicyRepository;
        public VehiclePolicyService(IVehiclePolicyRepository vehiclePolicyRepository)
        {
            this.vehiclePolicyRepository = vehiclePolicyRepository;
        }

        public async Task<SO.VehiclePolicy?> Create(SO.VehiclePolicy model)
        {
            if (model == null) return null;

            var create = await vehiclePolicyRepository.Create(model);
            if (!create) return null;
            return model;
           
        }
    }
}
