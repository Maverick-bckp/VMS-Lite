using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly TapfinDbContext _dbContext;
        public ClientRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ClientDetail>> getAllAsync(int? countryId)
        {
            var clientList = await _dbContext.ClientDetails.AsQueryable().Where(w => w.IsActive == true && (countryId == null || w.CountryId == countryId))
                .Include(o => o.Country)
                .ThenInclude(o => o.Region)
                .Include(o => o.ClientAddress.Where(w => w.IsActive == true))
                .Include(o => o.ClientRevenue.Where(w => w.IsActive == true))
                .Include(o => o.AspNetUsers.Where(w => w.IsActive == true))
                .ToListAsync();
            return clientList;
        }

        public async Task<ClientDetail> getClientDetailsById(int clientId, int? countryId = null)
        {
            var clientDetails = await _dbContext.ClientDetails.AsQueryable().Where(w => w.IsActive == true && w.Id == clientId && (countryId == null || w.CountryId == countryId))
                .Include(o => o.Country)
                .ThenInclude(o => o.Region)
                .Include(o => o.ClientAddress.Where(w => w.IsActive == true))
                .Include(o => o.ClientRevenue.Where(w => w.IsActive == true))
                .Include(o => o.AspNetUsers.Where(w => w.IsActive == true))
                .FirstOrDefaultAsync();
            return clientDetails;
        }

        public async Task addClient(ClientDetail clientDetail)
        {
            await _dbContext.ClientDetails.AddAsync(clientDetail);
        }

        public async Task addClientAddress(ClientAddress clientAddress)
        {
            await _dbContext.ClientAddress.AddRangeAsync(clientAddress);
        }

        public async Task updateClientDetails(ClientDetail clientDetail)
        {
            var clientData = _dbContext.ClientDetails.AsQueryable().Where(w => w.IsActive == true && w.Id == clientDetail.Id).FirstOrDefault();
            if (clientData != null)
            {
                clientData.ClientCode = clientDetail.ClientCode;
                clientData.TIN = clientDetail.TIN;
                clientData.BusinessName = clientDetail.BusinessName;
                clientData.TradeName = clientDetail.TradeName;
                clientData.DateOfEstablishment = clientDetail.DateOfEstablishment;
                clientData.StateRegistration = clientDetail.StateRegistration;
                clientData.MunicipalRegistration = clientDetail.MunicipalRegistration;
                clientData.Site = clientDetail.Site;
                clientData.ClientSince = clientDetail.ClientSince;
                clientData.ClosureDate = clientDetail.ClosureDate;
                clientData.Observations = clientDetail.Observations;
                clientData.UpdatedAt = DateTime.UtcNow;
                clientData.UpdatedBy = clientDetail.UpdatedBy;

                _dbContext.Entry(clientData).State = EntityState.Modified;
            }
        }

        public async Task deleteClient(int clientId, string userCustomId)
        {
            var clientData = _dbContext.ClientDetails.AsQueryable().Where(w => w.IsActive == true && w.Id == clientId).FirstOrDefault();
            if (clientData != null)
            {
                clientData.IsActive = false;
                clientData.DeletedAt = DateTime.Now;
                clientData.DeletedBy = userCustomId;
            }
        }
    }
}
