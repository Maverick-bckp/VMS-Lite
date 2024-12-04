using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly TapfinDbContext _dbContext;
        public RegionRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Region>> getAllAsync()
        {
            var regions = await _dbContext.Region.AsQueryable().Where(w => w.IsActive == true).IgnoreAutoIncludes().ToListAsync();
            return regions;
        }

        public async Task<Region> getRegionById(int regionId)
        {
            var region = await _dbContext.Region.AsQueryable().Where(w => w.IsActive == true && w.Id == regionId).FirstOrDefaultAsync();
            return region;
        }
        public async Task addRegion(Region region)
        {
            await _dbContext.Region.AddAsync(region);
        }

        public async Task deleteRegion(int Id, string userCustomId)
        {
            var region = _dbContext.Region.AsQueryable().Where(w => w.IsActive == true && w.Id == Id).FirstOrDefault();
            if (region != null)
            {
                region.IsActive = false;
                region.DeletedAt = DateTime.Now;
                region.DeletedBy = userCustomId;
            }
        }

        public async Task updateRegion(Region region)
        {
            var regionData = _dbContext.Region.AsQueryable().Where(w => w.IsActive == true && w.Id == region.Id).FirstOrDefault();
            if (regionData != null)
            {
                regionData.RegionName = region.RegionName;
                regionData.RegionCode = region.RegionCode;
                regionData.UpdatedAt = DateTime.UtcNow;
                regionData.UpdatedBy = region.UpdatedBy;

                _dbContext.Entry(regionData).State = EntityState.Modified;
            }
        }
    }
}
