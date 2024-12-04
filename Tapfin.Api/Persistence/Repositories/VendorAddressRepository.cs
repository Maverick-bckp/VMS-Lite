using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Models;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class VendorAddressRepository : IVendorAddressRepository
    {
        private readonly TapfinDbContext _dbContext;
        public VendorAddressRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<VendorAddress>> getAllAsync()
        {
            var vendorAddress = await _dbContext.VendorAddress.AsQueryable().Where(w => w.IsActive == true).ToListAsync();
            return vendorAddress;
        }

        public async Task<VendorAddress> getVendorAddressById(int addressId)
        {
            var vendorAddress = await _dbContext.VendorAddress.AsQueryable().Where(w => w.IsActive == true && w.Id == addressId).FirstOrDefaultAsync();
            return vendorAddress;
        }

        public async Task addVendorAddress(VendorAddress vendorAddress)
        {
            await _dbContext.VendorAddress.AddRangeAsync(vendorAddress);
        }

        public async Task updateVendorAddress(VendorAddress vendorAddress)
        {
            var vendorAddressData = _dbContext.VendorAddress.AsQueryable().Where(w => w.IsActive == true && w.Id == vendorAddress.Id).FirstOrDefault();
            if (vendorAddressData != null)
            {
                vendorAddressData.Location = vendorAddress.Location;
                vendorAddressData.ZipCode = vendorAddress.ZipCode;
                vendorAddressData.Street = vendorAddress.Street;
                vendorAddressData.Number = vendorAddress.Number;
                vendorAddressData.Neighbourhood = vendorAddress.Neighbourhood;
                vendorAddressData.State = vendorAddress.State;
                vendorAddressData.City = vendorAddress.City;
                vendorAddressData.Complement = vendorAddress.Complement;
                vendorAddressData.UpdatedAt = DateTime.UtcNow;
                vendorAddressData.UpdatedBy = vendorAddress.UpdatedBy;

                _dbContext.Entry(vendorAddressData).State = EntityState.Modified;
            }
        }

        public async Task deleteVendorAddress(int Id, string userCustomId)
        {
            var vendorAddressData = _dbContext.VendorAddress.AsQueryable().Where(w => w.IsActive == true && w.Id == Id).FirstOrDefault();
            if (vendorAddressData != null)
            {
                vendorAddressData.IsActive = false;
                vendorAddressData.DeletedAt = DateTime.Now;
                vendorAddressData.DeletedBy = userCustomId;
            }
        }
    }
}
