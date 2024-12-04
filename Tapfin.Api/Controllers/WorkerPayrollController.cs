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
    public class WorkerPayrollController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        private readonly IWorkerPayrollServices _workerPayrollServices;

        public WorkerPayrollController(IUnitOfWork uow, IMapper mapper, IWorkerPayrollServices workerPayrollServices)
        {
            _uow = uow;
            _mapper = mapper;
            _workerPayrollServices = workerPayrollServices;
        }


        [HttpGet]
        [Route("Worker/Payroll/Download")]
        [Authorize()]
        public async Task<dynamic> getWorkerListByClientIDToDownloadFormat([FromQuery]GetPayrollWorkerListRequest request)
        {
            var result = await _workerPayrollServices.getWorkerListByClientIDToDownloadFormat(request);
            return result;
        }

        [HttpPost]
        [Route("Worker/Payroll/Upload")]
        [Authorize()]
        public async Task<dynamic> UploadWorkerPayrollDetails([FromForm] UploadPayrollWorkerDetailsRequest request)
        {
            var result = await _workerPayrollServices.UploadWorkerPayrollDetails(request);
            return result;
        }
    }
}
