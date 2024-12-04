using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class WorkerEvaluationController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        private readonly IWorkerEvaluationServices _workerEvaluationServices;

        public WorkerEvaluationController(IUnitOfWork uow, IMapper mapper, IWorkerEvaluationServices workerEvaluationServices)
        {
            _uow = uow;
            _mapper = mapper;
            _workerEvaluationServices = workerEvaluationServices;
        }

        [HttpGet]
        [Route("Worker/Evaluation/List")]
        [Authorize()]
        public async Task<dynamic> getAll(int workerId)
        {
            var result = await _workerEvaluationServices.getAll(workerId);
            return result;
        }

        [HttpGet]
        [Route("Worker/Evaluation/{Id}")]
        [Authorize()]
        public async Task<dynamic> getById(int Id)
        {
            var result = await _workerEvaluationServices.getById(Id);
            return result;
        }

        [HttpPost]
        [Route("Worker/Evaluation/Create")]
        [Authorize(Roles = "HiringManager")]
        public async Task<dynamic> createWorkerEvaluation([FromBody] CreateWorkerEvaluationDetailsDtoRequest request)
        {
            var result = await _workerEvaluationServices.createWorkerEvaluation(request);
            return result;
        }

        [HttpPut]
        [Route("Worker/Evaluation/Update")]
        [Authorize(Roles = "HiringManager")]
        public async Task<dynamic> updateWorkerEvaluation([FromBody] UpdateWorkerEvaluationDetailsDtoRequest request)
        {
            var result = await _workerEvaluationServices.updateWorkerEvaluation(request);
            return result;
        }

        [HttpDelete]
        [Route("Worker/Evaluation/Delete/{Id}")]
        [Authorize(Roles = "HiringManager")]
        public async Task<dynamic> deleteWorkerEvaluation(int Id)
        {
            var result = await _workerEvaluationServices.deleteWorkerEvaluation(Id);
            return result;
        }
    }
}
