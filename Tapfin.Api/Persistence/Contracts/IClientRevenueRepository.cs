using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IClientRevenueRepository
    {
        Task<List<ClientRevenue>> getAllAsync(int clientId);
        Task<ClientRevenue> getClientRevenueById(int clientReveueId);
        Task addClientRevenue(ClientRevenue clientRevenue);
        Task updateClientRevenueDetails(ClientRevenue clientRevenue);
        Task deleteClientRevenue(int clientReveueId, string userCustomId);
    }
}
