using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IVendorAddressRepository
    {
        Task<List<VendorAddress>> getAllAsync();
        Task<VendorAddress> getVendorAddressById(int addressId);
        Task addVendorAddress(VendorAddress vendorAddress);
        Task updateVendorAddress(VendorAddress vendorAddress);
        Task deleteVendorAddress(int Id, string userCustomId);
    }
}
