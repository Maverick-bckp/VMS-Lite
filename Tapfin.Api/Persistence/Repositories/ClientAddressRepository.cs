using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class ClientAddressRepository : IClientAddressRepository
    {
        private readonly TapfinDbContext _dbContext;
        public ClientAddressRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ClientAddress>> getAllAsync()
        {
            var clientAddress = await _dbContext.ClientAddress.AsQueryable().Where(w => w.IsActive == true).ToListAsync();
            return clientAddress;
        }

        public async Task<ClientAddress> getClientAddressById(int addressId)
        {
            var clientAddress = await _dbContext.ClientAddress.AsQueryable().Where(w => w.IsActive == true && w.Id == addressId).FirstOrDefaultAsync();
            return clientAddress;
        }

        public async Task addClientAddress(ClientAddress clientAddress)
        {
            await _dbContext.ClientAddress.AddRangeAsync(clientAddress);
        }

        public async Task updateClientAddress(ClientAddress clientAddress)
        {
            var clientAddressData = _dbContext.ClientAddress.AsQueryable().Where(w => w.IsActive == true && w.Id == clientAddress.Id).FirstOrDefault();
            if (clientAddressData != null)
            {
                clientAddressData.Location = clientAddress.Location;
                clientAddressData.ZipCode = clientAddress.ZipCode;
                clientAddressData.Street = clientAddress.Street;
                clientAddressData.Number = clientAddress.Number;
                clientAddressData.Neighbourhood = clientAddress.Neighbourhood;
                clientAddressData.State = clientAddress.State;
                clientAddressData.City = clientAddress.City;
                clientAddressData.Complement = clientAddress.Complement;
                clientAddressData.UpdatedAt = DateTime.UtcNow;
                clientAddressData.UpdatedBy = clientAddress.UpdatedBy;

                _dbContext.Entry(clientAddressData).State = EntityState.Modified;
            }
        }

        public async Task deleteClientAddress(int Id, string userCustomId)
        {
            var clientAddressData = _dbContext.ClientAddress.AsQueryable().Where(w => w.IsActive == true && w.Id == Id).FirstOrDefault();
            if (clientAddressData != null)
            {
                clientAddressData.IsActive = false;
                clientAddressData.DeletedAt = DateTime.Now;
                clientAddressData.DeletedBy = userCustomId;
            }
        }
    }
}
