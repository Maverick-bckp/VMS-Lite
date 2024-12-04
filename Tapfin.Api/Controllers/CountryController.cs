using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tapfin.Api.Enums;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Services.Contracts;
using Tapfin.Api.Services.ServiceLogic;

namespace Tapfin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryServices _countryServices;

        public CountryController(ICountryServices countryServices)
        {
            _countryServices = countryServices;
        }

        [HttpGet]
        [Route("List")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> getAllCountry()
        {
            var result = await _countryServices.getAllCountry();
            return result;
        }

        [HttpGet]
        [Route("{Id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> getById(int Id)
        {
            var result = await _countryServices.getCountryById(Id);
            return result;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> createCountry([FromBody] CreateCountryDtoRequest request)
        {
            var result = await _countryServices.createCountry(request);
            return result;
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> updateCountry([FromBody] UpdateCountryDtoRequest request)
        {
            var result = await _countryServices.updateCountry(request);
            return result;
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> deleteCountry(int Id)
        {
            var result = await _countryServices.deleteCountry(Id);
            return result;
        }

        [HttpGet]
        [Route("Currency")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> getCurrency()
        {
            var result = await _countryServices.getCurrency();
            return result;
        }


    }
}
