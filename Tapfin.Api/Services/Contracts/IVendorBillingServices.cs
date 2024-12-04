using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Services.Contracts
{
    public interface IVendorBillingServices
    {
        Task<dynamic> getAll(int vendorId);
        Task<dynamic> getVendorBillingById(int Id);
        Task<dynamic> createVendorBilling(CreateVendorBillingDtoRequest request);
        Task<dynamic> updateVendorBillingDetails(UpdateVendorBillingDtoRequest request);
        Task<dynamic> deleteVendorBilling(int Id);
    }
}
