using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class WorkerEvaluationRepository : IWorkerEvaluationRepository
    {
        private readonly TapfinDbContext _dbContext;
        public WorkerEvaluationRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WorkerEvaluation>> getAllAsync(int workerId)
        {
            var workerEvaluationList = await _dbContext.WorkerEvaluation.AsQueryable().Where(w => w.IsActive == true && w.WorkerId == workerId)
                .ToListAsync();
            return workerEvaluationList;
        }

        public async Task<WorkerEvaluation> getWorkerEvaluationDetailsById(int id)
        {
            var workerEvaluationDetails = await _dbContext.WorkerEvaluation.AsQueryable().Where(w => w.IsActive == true && w.Id == id)
                .FirstOrDefaultAsync();
            return workerEvaluationDetails;
        }

        public async Task addWorkerEvaluationDetails(WorkerEvaluation workerEvaluation)
        {
            await _dbContext.WorkerEvaluation.AddAsync(workerEvaluation);
        }

        public async Task updateWorkerEvaluationDetails(WorkerEvaluation workerEvaluation)
        {

            var workerEvaluationData = _dbContext.WorkerEvaluation.AsQueryable().Where(w => w.IsActive == true && w.Id == workerEvaluation.Id).FirstOrDefault();

            if (workerEvaluationData != null)
            {
                workerEvaluationData.Evaluator = workerEvaluation.Evaluator;
                workerEvaluationData.Message = workerEvaluation.Message;
                workerEvaluationData.Date = workerEvaluation.Date;
                workerEvaluationData.Rating = workerEvaluation.Rating;
                workerEvaluationData.UpdatedAt = DateTime.UtcNow;
                workerEvaluationData.UpdatedBy = workerEvaluation.UpdatedBy;

                _dbContext.Entry(workerEvaluationData).State = EntityState.Modified;
            }
        }

        public async Task deleteWorkerEvaluation(int Id, string userCustomId)
        {
            var workerEvaluationData = _dbContext.WorkerEvaluation.AsQueryable().Where(w => w.IsActive == true && w.Id == Id).FirstOrDefault();

            if (workerEvaluationData != null)
            {
                workerEvaluationData.IsActive = false;
                workerEvaluationData.DeletedAt = DateTime.Now;
                workerEvaluationData.DeletedBy = userCustomId;
            }
        }
    }
}
