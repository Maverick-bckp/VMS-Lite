using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Models;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class VendorBillingRepository : IVendorBillingRepository
    {
        private readonly TapfinDbContext _dbContext;
        public VendorBillingRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<VendorBilling>> getAllAsync(int vendorId)
        {
            var vendorBillingList = await _dbContext.VendorBilling.AsQueryable().Where(w => w.IsActive == true && w.VendorId == vendorId)
                .Include(o=> o.VendorDetail)
                .ToListAsync();
            return vendorBillingList;
        }

        public async Task<VendorBilling> getClientRevenueById(int clientReveueId)
        {
            var vendorBillingDetails = await _dbContext.VendorBilling.AsQueryable().Where(w => w.IsActive == true && w.Id == clientReveueId)
                .Include(o => o.VendorDetail)
                .FirstOrDefaultAsync();

            return vendorBillingDetails;
        }

        public async Task addVendorBilling(VendorBilling vendorBilling)
        {
            await _dbContext.VendorBilling.AddAsync(vendorBilling);
        }

        public async Task updateVendorBillingDetails(VendorBilling vendorBilling)
        {
            var vendorBillingData = _dbContext.VendorBilling.AsQueryable().Where(w => w.IsActive == true && w.Id == vendorBilling.Id).FirstOrDefault();
            if (vendorBillingData != null)
            {
                vendorBillingData.ServiceFee = vendorBilling.ServiceFee;
                vendorBillingData.CommercialTerms = vendorBilling.CommercialTerms;
                vendorBillingData.Date = vendorBilling.Date;
                vendorBillingData.UpdatedAt = DateTime.UtcNow;
                vendorBillingData.UpdatedBy = vendorBilling.UpdatedBy;

                _dbContext.Entry(vendorBillingData).State = EntityState.Modified;
            }
        }

        public async Task deleteVendorBilling(int vendorBillingId, string userCustomId)
        {
            var vendorBillingIdData = _dbContext.VendorBilling.AsQueryable().Where(w => w.IsActive == true && w.Id == vendorBillingId).FirstOrDefault();
            if (vendorBillingIdData != null)
            {
                vendorBillingIdData.IsActive = false;
                vendorBillingIdData.DeletedAt = DateTime.Now;
                vendorBillingIdData.DeletedBy = userCustomId;
            }
        }
    }
}
