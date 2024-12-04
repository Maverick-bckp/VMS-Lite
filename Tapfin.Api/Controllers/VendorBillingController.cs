using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Tapfin.Api.Models.DTO.ClientRevenueDto;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;
using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class VendorBillingController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        private readonly IVendorBillingServices _vendorBillingServices;

        public VendorBillingController(IUnitOfWork uow, IMapper mapper, IVendorBillingServices vendorBillingServices)
        {
            _uow = uow;
            _mapper = mapper;
            _vendorBillingServices = vendorBillingServices;
        }

        [HttpGet]
        [Route("Vendor/Billing/List")]
        [Authorize()]
        public async Task<dynamic> getAll(int vendorId)
        {
            var result = await _vendorBillingServices.getAll(vendorId);
            return result;
        }

        [HttpGet]
        [Route("Vendor/Billing/{Id}")]
        [Authorize()]
        public async Task<dynamic> getVendorBillingById(int Id)
        {
            var result = await _vendorBillingServices.getVendorBillingById(Id);
            return result;
        }

        [HttpPost]
        [Route("Vendor/Billing/Create")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> createVendorBilling([FromBody] CreateVendorBillingDtoRequest request)
        {
            var result = await _vendorBillingServices.createVendorBilling(request);
            return result;
        }

        [HttpPut]
        [Route("Vendor/Billing/Update")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> updateVendorBillingDetails([FromBody] UpdateVendorBillingDtoRequest request)
        {
            var result = await _vendorBillingServices.updateVendorBillingDetails(request);
            return result;
        }

        [HttpDelete]
        [Route("Vendor/Billing/Delete/{Id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> deleteVendorBilling(int Id)
        {
            var result = await _vendorBillingServices.deleteVendorBilling(Id);
            return result;
        }
    }
}
