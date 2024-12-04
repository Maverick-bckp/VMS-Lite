using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly TapfinDbContext _dbContext;
        public VendorRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<VendorDetail>> getAllAsync(int? countryId)
        {
            var vendorList = await _dbContext.VendorDetails.AsQueryable().Where(w => w.IsActive == true && (countryId == null || w.CountryId == countryId))
                .Include(o => o.Country)
                .ThenInclude(o => o.Region)
                .Include(o => o.VendorAddress.Where(w => w.IsActive == true))
                .Include(o => o.VendorBilling.Where(w => w.IsActive == true))
                .Include(o => o.AspNetUsers.Where(w => w.IsActive == true))
                .ToListAsync();
            return vendorList;
        }

        public async Task<VendorDetail> getVendorDetailsById(int vendorId, int? countryId = null)
        {
            var vendorDetails = await _dbContext.VendorDetails.AsQueryable().Where(w => w.IsActive == true && w.Id == vendorId && (countryId == null || w.CountryId == countryId))
                .Include(o => o.Country)
                .ThenInclude(o => o.Region)
                .Include(o => o.VendorAddress.Where(w => w.IsActive == true))
                .Include(o => o.VendorBilling.Where(w => w.IsActive == true))
                .Include(o => o.AspNetUsers.Where(w => w.IsActive == true))
                .FirstOrDefaultAsync();
            return vendorDetails;
        }

        public async Task addVendor(VendorDetail vendorDetail)
        {
            await _dbContext.VendorDetails.AddAsync(vendorDetail);
        }

        public async Task deleteVendor(int vendorId)
        {
            var vendorData = _dbContext.VendorDetails.AsQueryable().Where(w => w.IsActive == true && w.Id == vendorId).FirstOrDefault();
            if (vendorData != null)
            {
                vendorData.IsActive = false;
                vendorData.DeletedAt = DateTime.Now;
            }
        }
        
        public async Task updateVendorDetails(VendorDetail vendorDetail)
        {
            var vendorData = _dbContext.VendorDetails.AsQueryable().Where(w => w.IsActive == true && w.Id == vendorDetail.Id).FirstOrDefault();
            if (vendorData != null)
            {
                vendorData.VendorCode = vendorDetail.VendorCode;
                vendorData.StartOfServiceProvision = vendorDetail.StartOfServiceProvision;
                vendorData.EndOfServiceProvision = vendorDetail.EndOfServiceProvision;
                vendorData.ContractNo = vendorDetail.ContractNo;
                vendorData.ServiceDescription = vendorDetail.ServiceDescription;
                vendorData.NationalRegistryOfLegalEntities = vendorDetail.NationalRegistryOfLegalEntities;
                vendorData.BusinessName = vendorDetail.BusinessName;
                vendorData.TradeName = vendorDetail.TradeName;
                vendorData.DateOfEstablishment = vendorDetail.DateOfEstablishment;
                vendorData.StateRegistration = vendorDetail.StateRegistration;
                vendorData.MunicipalRegistration = vendorDetail.MunicipalRegistration;
                vendorData.Site = vendorDetail.Site;
                vendorData.ClientSince = vendorDetail.ClientSince;
                vendorData.ClosureDate = vendorDetail.ClosureDate;
                vendorData.Observations = vendorDetail.Observations;
                vendorData.DefaultVendor = vendorDetail.DefaultVendor;
                vendorData.UpdatedAt = DateTime.UtcNow;
                vendorData.UpdatedBy = vendorDetail.UpdatedBy;

                _dbContext.Entry(vendorData).State = EntityState.Modified;
            }
        }
    }
}
