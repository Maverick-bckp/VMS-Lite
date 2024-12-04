using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IWorkerEvaluationRepository
    {
        Task<List<WorkerEvaluation>> getAllAsync(int workerId);
        Task<WorkerEvaluation> getWorkerEvaluationDetailsById(int id);
        Task addWorkerEvaluationDetails(WorkerEvaluation workerEvaluation);
        Task updateWorkerEvaluationDetails(WorkerEvaluation workerEvaluation);
        Task deleteWorkerEvaluation(int Id, string userCustomId);
    }
}
