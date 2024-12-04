using Tapfin.Api.Models.Entities;
using Tapfin.Api.Models;
using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class AllocatedAtClientRepository : IAllocatedAtClientRepository
    {
        private readonly TapfinDbContext _dbContext;
        public AllocatedAtClientRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AllocatedAtClient>> getAllAsync()
        {
            var allocatedAtClientTypes = await _dbContext.AllocatedAtClient.AsQueryable().Where(w => w.IsActive == true).ToListAsync();
            return allocatedAtClientTypes;
        }
    }
}
