using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : ControllerBase
    {
        private readonly IServiceTypesServices _serviceTypesServices;

        public ServiceTypesController(IServiceTypesServices serviceTypesServices)
        {
            _serviceTypesServices = serviceTypesServices;
        }

        [HttpGet]
        [Route("List")]
        [Authorize()]
        public async Task<dynamic> getAllDepartment()
        {
            var result = await _serviceTypesServices.getAllServiceTypes();
            return result;
        }
    }
}
