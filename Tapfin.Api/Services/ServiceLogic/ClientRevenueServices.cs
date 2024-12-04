using AutoMapper;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;
using static Tapfin.Api.Models.DTO.ClientRevenueDto;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class ClientRevenueServices : IClientRevenueServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public ClientRevenueServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAll(int clientId)
        {
            var clientRevenueList = await _uow.clientRevenueRepository.getAllAsync(clientId);
            dynamic clientRevenueListMapped = _mapper.Map<List<GetClientRevenueDtoResponse>>(clientRevenueList);

            if (clientRevenueList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, clientRevenueListMapped, "All Client Revenue details has been fetched successfully.");
        }

        public async Task<dynamic> getClientRevenueById(int Id)
        {
            var clientRevenue = await _uow.clientRevenueRepository.getClientRevenueById(Id);
            dynamic clientRevenueMapped = _mapper.Map<GetClientRevenueDtoResponse>(clientRevenue);

            if (clientRevenue == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, clientRevenueMapped, "Client Revenue details has been fetched successfully.");
        }

        public async Task<dynamic> createClientRevenue(CreateClientRevenueDtoRequest request)
        {
            var clientRevenue = _mapper.Map<ClientRevenue>(request);
            clientRevenue.CreatedBy = _currentUser.UserCustomId;

            await _uow.clientRevenueRepository.addClientRevenue(clientRevenue);
            int status = (int)await _uow.SaveChangesAsync();

            dynamic createdclientRevenueMapped = _mapper.Map<GetClientRevenueDtoResponse>(clientRevenue);

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Client Revenue details is not created. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.Created, message: "Client Revenue details is created successfully.");
        }

        public async Task<dynamic> updateClientRevenueDetails(UpdateClientRevenueDtoRequest request)
        {
            var clientRevenue = _mapper.Map<ClientRevenue>(request);
            clientRevenue.UpdatedBy = _currentUser.UserCustomId;

            await _uow.clientRevenueRepository.updateClientRevenueDetails(clientRevenue);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Client Revenue details is not updated. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Client Revenue details is updated successfully.");
        }

        public async Task<dynamic> deleteClientRevenue(int Id)
        {
            var userCustomId = _currentUser.UserCustomId;

            await _uow.clientRevenueRepository.deleteClientRevenue(Id, userCustomId);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Client Revenue details is not deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Client Revenue details is deleted successfully.");
        }
    }
}
