using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tapfin.Api.Filters;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tapfin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        private readonly IRegionServices _regionServices;

        public RegionController(IUnitOfWork uow, IMapper mapper, IRegionServices regionServices)
        {
            _uow = uow;
            _mapper = mapper;
            _regionServices = regionServices;
        }

        [HttpGet]
        [Route("List")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> getAllRegion()
        {
            var result = await _regionServices.getAllRegion();
            return result;
        }

        [HttpGet]
        [Route("{Id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> getById(int Id)
        {
            var result = await _regionServices.getRegionById(Id);
            return result;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> createRegion([FromBody] CreateRegionDtoRequest request)
        {
            var result = await _regionServices.createRegion(request);
            return result;
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> updateRegion([FromBody] UpdateRegionDtoRequest request)
        {
            var result = await _regionServices.updateRegion(request);
            return result;
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> deleteRegion(int Id)
        {
            var result = await _regionServices.deleteRegion(Id);
            return result;
        }
    }
}
