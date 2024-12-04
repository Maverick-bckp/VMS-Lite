using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class WorkerExtensionRepository : IWorkerExtensionRepository
    {
        private readonly TapfinDbContext _dbContext;
        public WorkerExtensionRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WorkerExtension>> getAllAsync(int workerId)
        {
            var workerExtensionList = await _dbContext.WorkerExtension.AsQueryable().Where(w => w.IsActive == true && w.WorkerId == workerId)
                .Include(o => o.WorkerExtensionStatusTypes)
                .ToListAsync();
            return workerExtensionList;
        }

        public async Task<List<WorkerExtensionStatusTypes>> getAllStatusTypesAsync()
        {
            var workerExtensionStatusTypesList = await _dbContext.WorkerExtensionStatusTypes.AsQueryable().Where(w => w.IsActive == true)
                .ToListAsync();
            return workerExtensionStatusTypesList;
        }

        public async Task<WorkerExtension> getWorkerExtensionDetailsById(int id)
        {
            var workerExtensionDetails = await _dbContext.WorkerExtension.AsQueryable().Where(w => w.IsActive == true && w.Id == id)
                .Include(o => o.WorkerExtensionStatusTypes)
                .FirstOrDefaultAsync();
            return workerExtensionDetails;
        }

        public async Task addWorkerExtensionDetails(WorkerExtension workerExtension)
        {
            await _dbContext.WorkerExtension.AddAsync(workerExtension);
        }

        public async Task updateWorkerExtensionDetails(WorkerExtension workerExtension)
        {

            var workerExtensionData = _dbContext.WorkerExtension.AsQueryable().Where(w => w.IsActive == true && w.Id == workerExtension.Id && w.WorkerId == workerExtension.WorkerId).FirstOrDefault();

            if (workerExtensionData != null)
            {
                workerExtensionData.Message = workerExtension.Message;
                workerExtensionData.ExtensionDate = workerExtension.ExtensionDate;
                workerExtensionData.Status = workerExtension.Status;
                workerExtensionData.UpdatedAt = DateTime.UtcNow;
                workerExtensionData.UpdatedBy = workerExtension.UpdatedBy;

                _dbContext.Entry(workerExtensionData).State = EntityState.Modified;
            }
        }

        public async Task deleteWorkerExtension(int Id, string userCustomId)
        {
            var workerExtensionData = _dbContext.WorkerExtension.AsQueryable().Where(w => w.IsActive == true && w.Id == Id).FirstOrDefault();

            if (workerExtensionData != null)
            {
                workerExtensionData.IsActive = false;
                workerExtensionData.DeletedAt = DateTime.Now;
                workerExtensionData.DeletedBy = userCustomId;
            }
        }
    }
}
