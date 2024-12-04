using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Services.Contracts
{
    public interface IClientServices
    {
        Task<dynamic> getAllClient();
        Task<dynamic> getClientById(int Id);
        Task<dynamic> createClient(CreateClientDtoRequest request);
        Task<dynamic> updateClient(UpdateClientDtoRequest request);
        Task<dynamic> deleteClient(int Id);
        Task<dynamic> deleteClientAddress(int id);
    }
}
