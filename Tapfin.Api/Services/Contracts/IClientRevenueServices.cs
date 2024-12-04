using static Tapfin.Api.Models.DTO.ClientRevenueDto;

namespace Tapfin.Api.Services.Contracts
{
    public interface IClientRevenueServices
    {
        Task<dynamic> getAll(int clientId);
        Task<dynamic> getClientRevenueById(int Id);
        Task<dynamic> createClientRevenue(CreateClientRevenueDtoRequest request);
        Task<dynamic> updateClientRevenueDetails(UpdateClientRevenueDtoRequest request);
        Task<dynamic> deleteClientRevenue(int Id);
    }
}
