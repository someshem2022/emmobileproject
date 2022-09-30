
namespace EM.InsurePlus.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;   
    using AutoMapper;
    using EM.InsurePlus.Data.Contract;
    using EM.InsurePlus.Repository.Contract;
    using SO = EM.InsurePlus.Services.Models;
    using DO = EM.InsurePlus.Data.Models;
    public class VehiclePolicyRepository : IVehiclePolicyRepository
    {
        private readonly IMapper _mapper;
        private readonly IStorageContext _storageContext;
        public VehiclePolicyRepository(IStorageContext storageContext,IMapper mapper)
        {
            _storageContext = storageContext;
            _mapper = mapper;
        }

        public async Task<bool> Create(SO.VehiclePolicy model)
        {
            if (model == null) return false;

            var mappedModel = _mapper.Map<SO.VehiclePolicy, DO.VehiclePolicy>(model);
            _storageContext.Set<DO.VehiclePolicy>().Add(mappedModel);
            var result = await _storageContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
