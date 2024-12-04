using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;
using Tapfin.Api.Services.ServiceLogic;
using static Tapfin.Api.Models.DTO.ClientRevenueDto;

namespace Tapfin.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ClientRevenueController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        private readonly IClientRevenueServices _clientRevenueServices;

        public ClientRevenueController(IUnitOfWork uow, IMapper mapper, IClientRevenueServices clientRevenueServices)
        {
            _uow = uow;
            _mapper = mapper;
            _clientRevenueServices = clientRevenueServices;
        }

        [HttpGet]
        [Route("Client/Revenue/List")]
        [Authorize()]
        public async Task<dynamic> getAll(int clientId)
        {
            var result = await _clientRevenueServices.getAll(clientId);
            return result;
        }

        [HttpGet]
        [Route("Client/Revenue/{Id}")]
        [Authorize()]
        public async Task<dynamic> getClientRevenueById(int Id)
        {
            var result = await _clientRevenueServices.getClientRevenueById(Id);
            return result;
        }

        [HttpPost]
        [Route("Client/Revenue/Create")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> createClientRevenue([FromBody] CreateClientRevenueDtoRequest request)
        {
            var result = await _clientRevenueServices.createClientRevenue(request);
            return result;
        }

        [HttpPut]
        [Route("Client/Revenue/Update")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> updateClientRevenueDetails([FromBody] UpdateClientRevenueDtoRequest request)
        {
            var result = await _clientRevenueServices.updateClientRevenueDetails(request);
            return result;
        }

        [HttpDelete]
        [Route("Client/Revenue/Delete/{Id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> deleteClientRevenue(int Id)
        {
            var result = await _clientRevenueServices.deleteClientRevenue(Id);
            return result;
        }
    }
}
