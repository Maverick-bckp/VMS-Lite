using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class ClientRevenueRepository : IClientRevenueRepository
    {
        private readonly TapfinDbContext _dbContext;
        public ClientRevenueRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ClientRevenue>> getAllAsync(int clientId)
        {
            var clientReveueList = await _dbContext.ClientRevenue.AsQueryable().Where(w => w.IsActive == true && w.ClientId == clientId)
                .Include(o=> o.ClientDetail)
                .ToListAsync();
            return clientReveueList;
        }

        public async Task<ClientRevenue> getClientRevenueById(int clientReveueId)
        {
            var clientReveueDetails = await _dbContext.ClientRevenue.AsQueryable().Where(w => w.IsActive == true && w.Id == clientReveueId)
                .Include(o => o.ClientDetail)
                .FirstOrDefaultAsync();
            return clientReveueDetails;
        }

        public async Task addClientRevenue(ClientRevenue clientRevenue)
        {
            await _dbContext.ClientRevenue.AddAsync(clientRevenue);
        }

        public async Task updateClientRevenueDetails(ClientRevenue clientRevenue)
        {
            var clientRevenueData = _dbContext.ClientRevenue.AsQueryable().Where(w => w.IsActive == true && w.Id == clientRevenue.Id).FirstOrDefault();
            if (clientRevenueData != null)
            {
                clientRevenueData.ServiceFee = clientRevenue.ServiceFee;
                clientRevenueData.BusinessTerms = clientRevenue.BusinessTerms;
                clientRevenueData.Date = clientRevenue.Date;
                clientRevenueData.UpdatedAt = DateTime.UtcNow;
                clientRevenueData.UpdatedBy = clientRevenue.UpdatedBy;

                _dbContext.Entry(clientRevenueData).State = EntityState.Modified;
            }
        }

        public async Task deleteClientRevenue(int clientReveueId, string userCustomId)
        {
            var clientRevenueData = _dbContext.ClientRevenue.AsQueryable().Where(w => w.IsActive == true && w.Id == clientReveueId).FirstOrDefault();
            if (clientRevenueData != null)
            {
                clientRevenueData.IsActive = false;
                clientRevenueData.DeletedAt = DateTime.Now;
                clientRevenueData.DeletedBy = userCustomId;
            }
        }
    }
}
