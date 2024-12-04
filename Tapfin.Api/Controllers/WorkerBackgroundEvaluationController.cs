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
    public class WorkerBackgroundEvaluationController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        private readonly IWorkerBackgroundEvaluationServices _workerBackgroundEvaluationServices;

        public WorkerBackgroundEvaluationController(IUnitOfWork uow, IMapper mapper, IWorkerBackgroundEvaluationServices workerBackgroundEvaluationServices)
        {
            _uow = uow;
            _mapper = mapper;
            _workerBackgroundEvaluationServices = workerBackgroundEvaluationServices;
        }

        [HttpGet]
        [Route("Worker/BackgroundEvaluation/List")]
        [Authorize()]
        public async Task<dynamic> getAll(int workerId)
        {
            var result = await _workerBackgroundEvaluationServices.getAll(workerId);
            return result;
        }

        [HttpGet]
        [Route("Worker/BackgroundEvaluation/ValidationTypes")]
        [Authorize()]
        public async Task<dynamic> getAllValidationTypes()
        {
            var result = await _workerBackgroundEvaluationServices.getAllValidationTypes();
            return result;
        }

        [HttpGet]
        [Route("Worker/BackgroundEvaluation/{Id}")]
        [Authorize()]
        public async Task<dynamic> getById(int Id)
        {
            var result = await _workerBackgroundEvaluationServices.getById(Id);
            return result;
        }

        [HttpPost]
        [Route("Worker/BackgroundEvaluation/Create")]
        [Authorize(Roles = "AccountManager")]
        public async Task<dynamic> createWorkerBckgEvaluation([FromBody] CreateWorkerBckgEvaluationDetailsDtoRequest request)
        {
            var result = await _workerBackgroundEvaluationServices.createWorkerBckgEvaluation(request);
            return result;
        }

        [HttpPut]
        [Route("Worker/BackgroundEvaluation/Update")]
        [Authorize(Roles = "AccountManager")]
        public async Task<dynamic> updateWorkerBckgEvaluation([FromBody] UpdateWorkerBckgEvaluationDetailsDtoRequest request)
        {
            var result = await _workerBackgroundEvaluationServices.updateWorkerBckgEvaluation(request);
            return result;
        }

        [HttpDelete]
        [Route("Worker/BackgroundEvaluation/Delete/{Id}")]
        [Authorize(Roles = "AccountManager")]
        public async Task<dynamic> deleteWorkerBckgEvaluation(int Id)
        {
            var result = await _workerBackgroundEvaluationServices.deleteWorkerBckgEvaluation(Id);
            return result;
        }
    }
}
