using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IVendorRepository
    {
        Task<List<VendorDetail>> getAllAsync(int? countryId);
        Task addVendor(VendorDetail vendorDetail);
        Task updateVendorDetails(VendorDetail vendorDetail);
        Task deleteVendor(int vendorId);
        Task<VendorDetail> getVendorDetailsById(int vendorId, int? countryId = null);
    }
}
