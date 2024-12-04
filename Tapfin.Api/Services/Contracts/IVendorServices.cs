using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Services.Contracts
{
    public interface IVendorServices
    {
        Task<dynamic> getAllVendor();
        Task<dynamic> getVendorById(int Id);
        Task<dynamic> createVendor(CreateVendorDtoRequest request);
        Task<dynamic> updateVendor(UpdateVendorDetailsDtoRequest request);
        Task<dynamic> deleteVendor(int Id);
        Task<dynamic> deleteVendorAddress(int id);
    }
}
