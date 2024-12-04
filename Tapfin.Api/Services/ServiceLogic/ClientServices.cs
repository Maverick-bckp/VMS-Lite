using AutoMapper;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class ClientServices : IClientServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;
        private string _currentUserRole;

        public ClientServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
            _currentUserRole = _accountsServices.GetLoggedInUserRole();
        }
        public async Task<dynamic> getAllClient()
        {
            var countryId = _currentUser.CountryId;

            var clientList = await _uow.clientRepository.getAllAsync(countryId);

            if (clientList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            /*------ Map Client Data-------*/
            var clientListMapped = _mapper.Map<List<GetClientDtoResponse>>(clientList);

            /*-------- Map Region data in DTO --------*/
            foreach (var client in clientList)
            {
                var clientMapped = clientListMapped.FirstOrDefault(i => i.Id == client.Id);
                clientMapped.Region = _mapper.Map<ClientRegionDto>(client.Country.Region);
            }

            /*-------- Map User data in DTO --------*/
            foreach (var client in clientList)
            {
                var clientMapped = clientListMapped.FirstOrDefault(i => i.Id == client.Id);
                clientMapped.User = _mapper.Map<List<GetClientUsersDtoResponse>>(client.AspNetUsers);
            }

            return _result.AddReturnData(HttpStatusCode.OK, clientListMapped, "All clients data has been fetched successfully.");
        }

        public async Task<dynamic> getClientById(int Id)
        {
            var countryId = _currentUser.CountryId;

            var clientData = await _uow.clientRepository.getClientDetailsById(Id, countryId);

            if (clientData == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }


            var clientListMapped = _mapper.Map<GetClientDtoResponse>(clientData);
            clientListMapped.Region = _mapper.Map<ClientRegionDto>(clientData.Country.Region);
            clientListMapped.User = _mapper.Map<List<GetClientUsersDtoResponse>>(clientData.AspNetUsers);

            return _result.AddReturnData(HttpStatusCode.OK, clientListMapped, "Client data has been fetched successfully.");
        }

        public async Task<dynamic> createClient(CreateClientDtoRequest request)
        {

            /*------- Begin Transaction -------*/
            await _uow.BeginTransactionAsync();

            try
            {
                /*--------- Create Client Details ----------*/
                var clientData = _mapper.Map<ClientDetail>(request);
                clientData.CountryId = request.CountryId;
                clientData.CreatedBy = _currentUser.UserCustomId;
                await _uow.clientRepository.addClient(clientData);
                int addClientStatus = (int)await _uow.SaveChangesAsync();

                

                /*--------- Create Client Address ----------*/
                var clientAddressData = _mapper.Map<List<ClientAddress>>(request.Address);
                foreach (var clientAddress in clientAddressData)
                {
                    clientAddress.ClientId = clientData.Id;
                    clientAddress.CreatedBy = _currentUser.UserCustomId;
                    await _uow.clientRepository.addClientAddress(clientAddress);
                }
                int addClientAddressStatus = (int)await _uow.SaveChangesAsync();

                

                /*------- Commit Transaction -------*/
                await _uow.CommitAsync();


                /*------ Return Response -------*/
                if (addClientAddressStatus < 1)
                {
                    return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Client is not created. Please check details and try again.");
                }

                dynamic createdClientMapped = _mapper.Map<GetClientDtoResponse>(clientData);                

                return _result.AddReturnData(HttpStatusCode.Created, message: "Client details is created successfully.");

            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return _result.AddReturnData(HttpStatusCode.OK, message: ex.Message.ToString());
            }
        }

        public async Task<dynamic> updateClient(UpdateClientDtoRequest request)
        {

            /*------- Begin Transaction -------*/
            await _uow.BeginTransactionAsync();

            try
            {
                /*----------- Update Client Details-----------*/
                var clientData = _mapper.Map<ClientDetail>(request);
                clientData.UpdatedBy = _currentUser.UserCustomId;
                await _uow.clientRepository.updateClientDetails(clientData);
                int status = (int)await _uow.SaveChangesAsync();

                if (status < 1)
                {
                    return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Client data is not updated. Please check details and try again.");
                }


                /*--------- Create/Update Client Address ----------*/
                if (request.Address.Count > 0)
                {
                    foreach (var clientAddress in request.Address)
                    {
                        var clientAddressData = _mapper.Map<ClientAddress>(clientAddress);
                        var dbStatus = await ClientAddressInsertOrUpdate(clientAddressData, clientData.Id);

                        if (dbStatus < 1)
                        {
                            return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Client data is not updated. Please check details and try again.");
                        }
                    }
                }


                /*------- Commit Transaction -------*/
                await _uow.CommitAsync();

                return _result.AddReturnData(HttpStatusCode.OK, message: "Client data has been updated successfully.");

            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return _result.AddReturnData(HttpStatusCode.OK, message: ex.Message.ToString());
            }
        }

        public async Task<dynamic> deleteClient(int Id)
        {
            var userCustomId = _currentUser.UserCustomId;

            await _uow.clientRepository.deleteClient(Id, userCustomId);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Client data is not deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Client data is deleted successfully.");
        }

        public async Task<dynamic> deleteClientAddress(int id)
        {
            var userCustomId = _currentUser.UserCustomId;

            await _uow.ClientAddressRepository.deleteClientAddress(id, userCustomId);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Client address data is not deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Client address data is deleted successfully.");
        }

        private async Task<int> ClientAddressInsertOrUpdate(ClientAddress clientAddress, int clientId)
        {
            int status;
            var clientAddressData = await _uow.ClientAddressRepository.getClientAddressById(clientAddress.Id);


            if (clientAddressData == null) /*--- Insert Client Address ---*/
            {
                clientAddress.ClientId = clientId;
                clientAddress.CreatedBy = _currentUser.UserCustomId;
                await _uow.ClientAddressRepository.addClientAddress(clientAddress);
                status = (int)await _uow.SaveChangesAsync();
            }
            else                           /*--- Update Client Address ---*/
            {
                clientAddress.UpdatedBy = _currentUser.UserCustomId;
                await _uow.ClientAddressRepository.updateClientAddress(clientAddress);
                status = (int)await _uow.SaveChangesAsync();
            }

            return status;
        }
    }
}
