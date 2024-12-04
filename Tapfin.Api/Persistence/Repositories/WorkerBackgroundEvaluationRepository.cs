using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class WorkerBackgroundEvaluationRepository : IWorkerBackgroundEvaluationRepository
    {
        private readonly TapfinDbContext _dbContext;
        public WorkerBackgroundEvaluationRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WorkerBackgroundEvaluation>> getAllAsync(int workerId)
        {
            var workerBckgEvaluationList = await _dbContext.WorkerBackgroundEvaluation.AsQueryable().Where(w => w.IsActive == true && w.WorkerId == workerId)
                .ToListAsync();
            return workerBckgEvaluationList;
        }

        public async Task<List<WorkerBckgEvalValidationTypes>> getAllValidationTypesAsync()
        {
            var workerBckgEvaluationValidationTypesList = await _dbContext.ValidationTypes.AsQueryable().Where(w => w.IsActive == true)
                .ToListAsync();
            return workerBckgEvaluationValidationTypesList;
        }

        public async Task<WorkerBackgroundEvaluation> getWorkerBckgEvaluationDetailsById(int id)
        {
            var workerBckgEvaluationDetails = await _dbContext.WorkerBackgroundEvaluation.AsQueryable().Where(w => w.IsActive == true && w.Id == id)
                .FirstOrDefaultAsync();
            return workerBckgEvaluationDetails;
        }

        public async Task addWorkerBckgEvaluationDetails(WorkerBackgroundEvaluation workerBackgroundEvaluation)
        {
            await _dbContext.WorkerBackgroundEvaluation.AddAsync(workerBackgroundEvaluation);
        }

        public async Task updateWorkerBckgEvaluationDetails(WorkerBackgroundEvaluation workerBackgroundEvaluation)
        {

            var workerBckgEvaluationData = _dbContext.WorkerBackgroundEvaluation.AsQueryable().Where(w => w.IsActive == true && w.Id == workerBackgroundEvaluation.Id).FirstOrDefault();

            if (workerBckgEvaluationData != null)
            {
                workerBckgEvaluationData.Type = workerBackgroundEvaluation.Type;
                workerBckgEvaluationData.Institution = workerBackgroundEvaluation.Institution;
                workerBckgEvaluationData.Contact = workerBackgroundEvaluation.Contact;
                workerBckgEvaluationData.Telephone = workerBackgroundEvaluation.Telephone;
                workerBckgEvaluationData.StartDate = workerBackgroundEvaluation.StartDate;
                workerBckgEvaluationData.EndDate = workerBackgroundEvaluation.EndDate;
                workerBckgEvaluationData.Description = workerBackgroundEvaluation.Description;
                workerBckgEvaluationData.Validation = workerBackgroundEvaluation.Validation;
                workerBckgEvaluationData.UpdatedAt = DateTime.UtcNow;
                workerBckgEvaluationData.UpdatedBy = workerBackgroundEvaluation.UpdatedBy;

                _dbContext.Entry(workerBckgEvaluationData).State = EntityState.Modified;
            }
        }

        public async Task deleteWorkerBckgEvaluation(int Id, string userCustomId)
        {
            var workerBckgEvaluationData = _dbContext.WorkerBackgroundEvaluation.AsQueryable().Where(w => w.IsActive == true && w.Id == Id).FirstOrDefault();

            if (workerBckgEvaluationData != null)
            {
                workerBckgEvaluationData.IsActive = false;
                workerBckgEvaluationData.DeletedAt = DateTime.Now;
                workerBckgEvaluationData.DeletedBy = userCustomId;
            }
        }
    }
}
