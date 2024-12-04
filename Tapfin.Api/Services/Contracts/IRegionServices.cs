using Microsoft.AspNetCore.Mvc;
using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Services.Contracts
{
    public interface IRegionServices
    {
        Task<dynamic> getAllRegion();
        Task<dynamic> getRegionById(int Id);
        Task<dynamic> createRegion(CreateRegionDtoRequest request);
        Task<dynamic> updateRegion(UpdateRegionDtoRequest request);
        Task<dynamic> deleteRegion(int Id);
    }
}
