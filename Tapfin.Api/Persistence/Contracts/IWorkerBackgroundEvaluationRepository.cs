using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IWorkerBackgroundEvaluationRepository
    {
        Task<List<WorkerBackgroundEvaluation>> getAllAsync(int workerId);
        Task<List<WorkerBckgEvalValidationTypes>> getAllValidationTypesAsync();
        Task<WorkerBackgroundEvaluation> getWorkerBckgEvaluationDetailsById(int id);
        Task addWorkerBckgEvaluationDetails(WorkerBackgroundEvaluation workerBackgroundEvaluation);
        Task updateWorkerBckgEvaluationDetails(WorkerBackgroundEvaluation workerBackgroundEvaluation);
        Task deleteWorkerBckgEvaluation(int Id, string userCustomId);
    }
}
