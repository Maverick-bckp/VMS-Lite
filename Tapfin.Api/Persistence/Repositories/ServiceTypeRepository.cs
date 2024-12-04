using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly TapfinDbContext _dbContext;
        public ServiceTypeRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ServiceType>> getAllAsync()
        {
            var serviceTypes = await _dbContext.ServiceType.AsQueryable().Where(w => w.IsActive == true).ToListAsync();
            return serviceTypes;
        }
    }
}
