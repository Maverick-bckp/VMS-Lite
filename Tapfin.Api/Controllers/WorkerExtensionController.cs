using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;
using Tapfin.Api.Services.ServiceLogic;

namespace Tapfin.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class WorkerExtensionController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        private readonly IWorkerExtensionServices _workerExtensionServices;

        public WorkerExtensionController(IUnitOfWork uow, IMapper mapper, IWorkerExtensionServices workerExtensionServices)
        {
            _uow = uow;
            _mapper = mapper;
            _workerExtensionServices = workerExtensionServices;
        }


        [HttpGet]
        [Route("Worker/Extension/List")]
        [Authorize()]
        public async Task<dynamic> getAll(int workerId)
        {
            var result = await _workerExtensionServices.getAll(workerId);
            return result;
        }

        [HttpGet]
        [Route("Worker/Extension/{Id}")]
        [Authorize()]
        public async Task<dynamic> getById(int Id)
        {
            var result = await _workerExtensionServices.getById(Id);
            return result;
        }

        [HttpGet]
        [Route("Worker/Extension/StatusTypes")]
        [Authorize()]
        public async Task<dynamic> getAllExtensionStatusTypes()
        {
            var result = await _workerExtensionServices.getAllExtensionStatusTypes();
            return result;
        }

        [HttpPost]
        [Route("Worker/Extension/Create")]
        [Authorize(Roles = "HiringManager")]
        public async Task<dynamic> createWorkerExtension([FromBody] CreateWorkerExtensionDetailsDtoRequest request)
        {
            var result = await _workerExtensionServices.createWorkerExtension(request);
            return result;
        }

        [HttpPut]
        [Route("Worker/Extension/Update")]
        [Authorize(Roles = "HiringManager")]
        public async Task<dynamic> updateWorkerExtension([FromBody] UpdateWorkerExtensionDetailsDtoRequest request)
        {
            var result = await _workerExtensionServices.updateWorkerExtension(request);
            return result;
        }

        [HttpDelete]
        [Route("Worker/Extension/Delete/{Id}")]
        [Authorize(Roles = "HiringManager")]
        public async Task<dynamic> deleteWorkerExtension(int Id)
        {
            var result = await _workerExtensionServices.deleteWorkerExtension(Id);
            return result;
        }
    }
}
