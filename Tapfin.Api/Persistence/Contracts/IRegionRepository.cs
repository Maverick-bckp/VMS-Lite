using System.Collections;
using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IRegionRepository
    {
        Task<List<Region>> getAllAsync();
        Task addRegion(Region region);
        Task updateRegion(Region region);
        Task deleteRegion(int Id, string userCustomId);
        Task<Region> getRegionById(int regionId);
    }
}
