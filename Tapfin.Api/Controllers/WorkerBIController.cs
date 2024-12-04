using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerBIController : ControllerBase
    {
        private readonly IWorkerBIServices _workerBIServices;

        public WorkerBIController(IWorkerBIServices workerBIServices)
        {
            _workerBIServices = workerBIServices;
        }

        [HttpPost]
        [Route("Details")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> getAllBIData([FromBody] WorkerBIDetailsRequestDto request)
        {
            var result = await _workerBIServices.getAllBIData(request);
            return result;
        }
    }
}
