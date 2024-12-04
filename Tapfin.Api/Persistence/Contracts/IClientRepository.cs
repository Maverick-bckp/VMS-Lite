using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IClientRepository
    {
        Task<List<ClientDetail>> getAllAsync(int? countryId);
        Task addClient(ClientDetail clientDetail);
        Task addClientAddress(ClientAddress clientAddress);
        Task updateClientDetails(ClientDetail clientDetail);
        Task deleteClient(int clientId, string userCustomId);
        Task<ClientDetail> getClientDetailsById(int clientId, int? countryId = null);
    }
}
