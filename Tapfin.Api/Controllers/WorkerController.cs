using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        private readonly IWorkerServices _workerServices;

        public WorkerController(IUnitOfWork uow, IMapper mapper, IWorkerServices workerServices)
        {
            _uow = uow;
            _mapper = mapper;
            _workerServices = workerServices;
        }

        [HttpGet]
        [Route("List")]
        [Authorize()]
        public async Task<dynamic> getAll(int? jobId = null, int? jobOrderId = null)
        {
            var result = await _workerServices.getAll(jobId, jobOrderId);
            return result;
        }

        [HttpGet]
        [Route("{Id}")]
        [Authorize()]
        public async Task<dynamic> getWorkerDetailsById(int Id)
        {
            var result = await _workerServices.getWorkerDetailsById(Id);
            return result;
        }

        [HttpGet]
        [Route("Details/{Id}")]
        [Authorize()]
        public async Task<dynamic> getWorkerFullDetailsById(int Id)
        {
            var result = await _workerServices.getWorkerFullDetailsById(Id);
            return result;
        }

        [HttpGet]
        [Route("StatusTypes")]
        [Authorize()]
        public async Task<dynamic> getWorkerStatusType()
        {
            var result = await _workerServices.getWorkerStatusType();
            return result;
        }

        [HttpGet]
        [Route("Contract/Types")]
        [Authorize()]
        public async Task<dynamic> getWorkerContractStatusType()
        {
            var result = await _workerServices.getWorkerContractStatusType();
            return result;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "AccountManager")]
        public async Task<dynamic> createWorkerDetails([FromBody] CreateWorkerDetailsDtoRequest request)
        {
            var result = await _workerServices.createWorkerDetails(request);
            return result;
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "AccountManager")]
        public async Task<dynamic> updateWorkerDetails([FromBody] UpdateWorkerDetailsDtoRequest request)
        {
            var result = await _workerServices.updateWorkerDetails(request);
            return result;
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        [Authorize(Roles = "AccountManager")]
        public async Task<dynamic> deleteWorkerDetails(int Id)
        {
            var result = await _workerServices.deleteWorkerDetails(Id);
            return result;
        }
    }
}
