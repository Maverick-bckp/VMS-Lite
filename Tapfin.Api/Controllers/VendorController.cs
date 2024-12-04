using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Services.Contracts;
using Tapfin.Api.Services.ServiceLogic;

namespace Tapfin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorServices _vendorServices;

        public VendorController(IVendorServices vendorServices)
        {
            _vendorServices = vendorServices;
        }

        [HttpGet]
        [Route("List")]
        [Authorize()]
        public async Task<dynamic> getAllVendor()
        {
            var result = await _vendorServices.getAllVendor();
            return result;
        }

        [HttpGet]
        [Route("{Id}")]
        [Authorize()]
        public async Task<dynamic> getById(int Id)
        {
            var result = await _vendorServices.getVendorById(Id);
            return result;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> createVendor([FromBody] CreateVendorDtoRequest request)
        {
            var result = await _vendorServices.createVendor(request);
            return result;
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> updateVendor([FromBody] UpdateVendorDetailsDtoRequest request)
        {
            var result = await _vendorServices.updateVendor(request);
            return result;
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> deleteVendor(int Id)
        {
            var result = await _vendorServices.deleteVendor(Id);
            return result;
        }

        [HttpDelete]
        [Route("Address/Delete/{Id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> deleteVendorAddress(int Id)
        {
            var result = await _vendorServices.deleteVendorAddress(Id);
            return result;
        }
    }
}
