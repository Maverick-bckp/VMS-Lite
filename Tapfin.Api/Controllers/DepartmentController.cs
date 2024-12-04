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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentServices _departmentServices;

        public DepartmentController(IDepartmentServices departmentServices)
        {
           _departmentServices = departmentServices;
        }

        [HttpGet]
        [Route("List")]
        [Authorize()]
        public async Task<dynamic> getAllDepartment()
        {
            var result = await _departmentServices.getAllDepartment();
            return result;
        }

        [HttpGet]
        [Route("{Id}")]
        [Authorize()]
        public async Task<dynamic> getDepartmentById(int Id)
        {
            var result = await _departmentServices.getDepartmentById(Id);
            return result;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> createDepartment([FromBody] CreateDepartmentDtoRequest request)
        {
            var result = await _departmentServices.createDepartment(request);
            return result;
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> updateDepartment([FromBody] UpdateDepartmentDtoRequest request)
        {
            var result = await _departmentServices.updateDepartment(request);
            return result;
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> deleteDepartment(int Id)
        {
            var result = await _departmentServices.deleteDepartment(Id);
            return result;
        }
    }
}
