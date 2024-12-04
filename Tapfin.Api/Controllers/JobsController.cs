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
    public class JobsController : ControllerBase
    {
        private readonly IJobServices _jobServices;

        public JobsController(IJobServices jobServices)
        {
            _jobServices = jobServices;
        }

        [HttpGet]
        [Route("List")]
        [Authorize(Roles = "HiringManager,AccountManager")]
        public async Task<dynamic> getAllJobs()
        {
            var result = await _jobServices.getAllJobs();
            return result;
        }

        [HttpGet]
        [Route("{Id}")]
        [Authorize(Roles = "HiringManager,AccountManager")]
        public async Task<dynamic> getById(int Id)
        {
            var result = await _jobServices.getJobDetailsById(Id);
            return result;
        }

        [HttpGet]
        [Route("StatusTypes")]
        [Authorize()]
        public async Task<dynamic> getAllJobStatusTypes()
        {
            var result = await _jobServices.getAllJobStatusTypes();
            return result;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "HiringManager")]
        public async Task<dynamic> createJob([FromForm] CreateJobDtoRequest request)
        {
            var result = await _jobServices.createJob(request);
            return result;
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "HiringManager")]
        public async Task<dynamic> updateJobDetails([FromForm] UpdateJobDetailsDtoRequest request)
        {
            /*---*/
            var result = await _jobServices.updateJob(request);
            return result;
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        [Authorize(Roles = "HiringManager")]
        public async Task<dynamic> deleteJob(int Id)
        {
            var result = await _jobServices.deleteJob(Id);
            return result;
        }
    }
}
