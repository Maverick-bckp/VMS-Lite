using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.ViewModels;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class WorkerBIRepository : IWorkerBIRepository
    {
        private readonly TapfinDbContext _dbContext;
        public WorkerBIRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetWorkerCountPerDepartment>> getWorkerCountPerDepartment(int? countryId, int? clientId, int? vendorId)
        {
            var workerCountPerDepartmentList = await _dbContext.Set<GetWorkerCountPerDepartment>().FromSql($"exec getWorkerCountPerDepartment {countryId},{clientId},{vendorId}").ToListAsync();

            return workerCountPerDepartmentList;
        }
    }
}
