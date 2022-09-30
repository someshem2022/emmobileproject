

namespace EM.InsurePlus.Services.Contract
{
    using SO = EM.InsurePlus.Services.Models;
    public interface IVehiclePolicyService
    {
        Task<SO.VehiclePolicy?> Create(SO.VehiclePolicy model);
    }
}
