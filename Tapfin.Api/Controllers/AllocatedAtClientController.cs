using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllocatedAtClientController : ControllerBase
    {
        private readonly IAllocatedAtClientServices _allocatedAtClientServices;

        public AllocatedAtClientController(IAllocatedAtClientServices allocatedAtClientServices)
        {
            _allocatedAtClientServices = allocatedAtClientServices;
        }

        [HttpGet]
        [Route("List")]
        [Authorize()]
        public async Task<dynamic> getAllDepartment()
        {
            var result = await _allocatedAtClientServices.getAllTypes();
            return result;
        }
    }
}
