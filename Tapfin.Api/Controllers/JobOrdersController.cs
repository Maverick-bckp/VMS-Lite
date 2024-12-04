using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobOrdersController : ControllerBase
    {
        private readonly IJobOrderServices _jobOrderServices;

        public JobOrdersController(IJobOrderServices jobOrderServices)
        {
            _jobOrderServices = jobOrderServices;
        }

        [HttpGet]
        [Route("List")]
        [Authorize(Roles = "VendorAdminOps,AccountManager,HiringManager")]
        public async Task<dynamic> getAllJobOrders([FromQuery]int jobId)
        {
            var result = await _jobOrderServices.getAllJobOrders(jobId);
            return result;
        }

        [HttpGet]
        [Route("StatusTypes/List")]
        [Authorize(Roles = "VendorAdminOps,AccountManager,HiringManager")]
        public async Task<dynamic> getAllJobOrderStatusTypes()
        {
            var result = await _jobOrderServices.getAllJobOrderStatusTypes();
            return result;
        }

        [HttpGet]
        [Route("{Id}")]
        [Authorize(Roles = "VendorAdminOps,AccountManager,HiringManager")]
        public async Task<dynamic> getById(int Id)
        {
            var result = await _jobOrderServices.getJobOrderById(Id);
            return result;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "VendorAdminOps,AccountManager")]
        public async Task<dynamic> createJobOrder([FromBody] CreateJobOrderDtoRequest request)
        {
            var result = await _jobOrderServices.createJobOrder(request);
            return result;
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "VendorAdminOps,AccountManager")]
        public async Task<dynamic> updateJobOrder([FromBody] UpdateJobOrderDtoRequest request)
        {
            var result = await _jobOrderServices.updateJobOrder(request);
            return result;
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        [Authorize(Roles = "VendorAdminOps,AccountManager")]
        public async Task<dynamic> deleteJobOrder(int Id)
        {
            var result = await _jobOrderServices.deleteJobOrder(Id);
            return result;
        }
    }
}
