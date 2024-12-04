using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IVendorBillingRepository
    {
        Task<List<VendorBilling>> getAllAsync(int vendorId);
        Task<VendorBilling> getClientRevenueById(int clientReveueId);
        Task addVendorBilling(VendorBilling vendorBilling);
        Task updateVendorBillingDetails(VendorBilling vendorBilling);
        Task deleteVendorBilling(int vendorBillingId, string userCustomId);
    }
}
