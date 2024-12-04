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
    public class ClientController : ControllerBase
    {
        private readonly IClientServices _clientServices;

        public ClientController(IClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        [HttpGet]
        [Route("List")]
        [Authorize()]
        public async Task<dynamic> getAllClient()
        {
            var result = await _clientServices.getAllClient();
            return result;
        }

        [HttpGet]
        [Route("{Id}")]
        [Authorize()]
        public async Task<dynamic> getClientById(int Id)
        {
            var result = await _clientServices.getClientById(Id);
            return result;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> createClient([FromBody] CreateClientDtoRequest request)
        {
            var result = await _clientServices.createClient(request);
            return result;
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> updateClient([FromBody] UpdateClientDtoRequest request)
        {
            var result = await _clientServices.updateClient(request);
            return result;
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> deleteClient(int Id)
        {
            var result = await _clientServices.deleteClient(Id);
            return result;
        }

        [HttpDelete]
        [Route("Address/Delete/{Id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> deleteClientAddress(int Id)
        {
            var result = await _clientServices.deleteClientAddress(Id);
            return result;
        }
    }
}
