using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IClientAddressRepository
    {
        Task<List<ClientAddress>> getAllAsync();
        Task<ClientAddress> getClientAddressById(int addressId);
        Task addClientAddress(ClientAddress clientAddress);
        Task updateClientAddress(ClientAddress clientAddress);
        Task deleteClientAddress(int Id, string userCustomId);
    }
}
