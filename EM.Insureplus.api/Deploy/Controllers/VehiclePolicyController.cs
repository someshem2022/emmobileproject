using AutoMapper;
using EM.InsurePlus.Api.Models;
using EM.InsurePlus.Services.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SO = EM.InsurePlus.Services.Models;
namespace EM.InsurePlus.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehiclePolicyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVehiclePolicyService vehiclePolicyService;
        public VehiclePolicyController(IMapper mapper, IVehiclePolicyService vehiclePolicyService)
        {
            this._mapper = mapper;
            this.vehiclePolicyService = vehiclePolicyService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VehiclePolicy model)
        {
            if (model == null) return BadRequest("Mandatory fields are empty");

            var policyModel = _mapper.Map<VehiclePolicy, SO.VehiclePolicy>(model);
            var result = await vehiclePolicyService.Create(policyModel);

            if (result is  null) return BadRequest("User enrollment for Pro subscription failed");

            return Ok(result);

        }
    }
}

