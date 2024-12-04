using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class WorkerEquipmentCostRepository : IWorkerEquipmentCostRepository
    {
        private readonly TapfinDbContext _dbContext;
        public WorkerEquipmentCostRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WorkerEquipmentCost>> getAllAsync(int workerId)
        {
            var workerEquipmentCostList = await _dbContext.WorkerEquipmentCost.AsQueryable().Where(w => w.IsActive == true && w.WorkerId == workerId)
                .Include(w => w.WorkerDetail)
                .ToListAsync();
            return workerEquipmentCostList;
        }

        public async Task<WorkerEquipmentCost> getWorkerEquipmentCostDetailsById(int id)
        {
            var workerEquipmentCostDetails = await _dbContext.WorkerEquipmentCost.AsQueryable().Where(w => w.IsActive == true && w.Id == id)
                .FirstOrDefaultAsync();
            return workerEquipmentCostDetails;
        }

        public async Task addWorkerEquipmentCostDetails(WorkerEquipmentCost workerEquipmentCost)
        {
            await _dbContext.WorkerEquipmentCost.AddAsync(workerEquipmentCost);
        }

        public async Task updateWorkerEquipmentCostDetails(WorkerEquipmentCost workerEquipmentCost)
        {

            var workerEquipmentCostData = _dbContext.WorkerEquipmentCost.AsQueryable().Where(w => w.IsActive == true && w.Id == workerEquipmentCost.Id).FirstOrDefault();

            if (workerEquipmentCostData != null)
            {
                workerEquipmentCostData.Reference = workerEquipmentCost.Reference;
                workerEquipmentCostData.Amount = workerEquipmentCost.Amount;
                workerEquipmentCostData.Remarks = workerEquipmentCost.Remarks;
                workerEquipmentCostData.Burden = workerEquipmentCost.Burden;
                workerEquipmentCostData.UpdatedAt = DateTime.UtcNow;
                workerEquipmentCostData.UpdatedBy = workerEquipmentCost.UpdatedBy;

                _dbContext.Entry(workerEquipmentCostData).State = EntityState.Modified;
            }
        }

        public async Task deleteWorkerEquipmentCost(int Id, string userCustomId)
        {
            var workerEquipmentCostData = _dbContext.WorkerEquipmentCost.AsQueryable().Where(w => w.IsActive == true && w.Id == Id).FirstOrDefault();

            if (workerEquipmentCostData != null)
            {
                workerEquipmentCostData.IsActive = false;
                workerEquipmentCostData.DeletedAt = DateTime.Now;
                workerEquipmentCostData.DeletedBy = userCustomId;
            }
        }
    }
}
