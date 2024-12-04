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
    public class WorkerEquipmentCostController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        private readonly IWorkerEquipmentCostServices _workerEquipmentCostServices;

        public WorkerEquipmentCostController(IUnitOfWork uow, IMapper mapper, IWorkerEquipmentCostServices workerEquipmentCostServices)
        {
            _uow = uow;
            _mapper = mapper;
            _workerEquipmentCostServices = workerEquipmentCostServices;
        }

        [HttpGet]
        [Route("Worker/EquipmentCost/List")]
        [Authorize()]
        public async Task<dynamic> getAll(int workerId)
        {
            var result = await _workerEquipmentCostServices.getAll(workerId);
            return result;
        }

        [HttpGet]
        [Route("Worker/EquipmentCost/{Id}")]
        [Authorize()]
        public async Task<dynamic> getById(int Id)
        {
            var result = await _workerEquipmentCostServices.getById(Id);
            return result;
        }

        [HttpPost]
        [Route("Worker/EquipmentCost/Create")]
        [Authorize(Roles = "AccountManager")]
        public async Task<dynamic> createWorkerEquipmentCost([FromBody] CreateWorkerEquipmentCostDetailsDtoRequest request)
        {
            var result = await _workerEquipmentCostServices.createWorkerEquipmentCost(request);
            return result;
        }

        [HttpPut]
        [Route("Worker/EquipmentCost/Update")]
        [Authorize(Roles = "AccountManager")]
        public async Task<dynamic> updateWorkerEquipmentCost([FromBody] UpdateWorkerEquipmentCostDetailsDtoRequest request)
        {
            var result = await _workerEquipmentCostServices.updateWorkerEquipmentCost(request);
            return result;
        }

        [HttpDelete]
        [Route("Worker/EquipmentCost/Delete/{Id}")]
        [Authorize(Roles = "AccountManager")]
        public async Task<dynamic> deleteWorkerEquipmentCost(int Id)
        {
            var result = await _workerEquipmentCostServices.deleteWorkerEquipmentCost(Id);
            return result;
        }
    }
}
