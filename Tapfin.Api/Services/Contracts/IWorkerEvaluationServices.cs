using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Services.Contracts
{
    public interface IWorkerEvaluationServices
    {
        Task<dynamic> getAll(int workerId);
        Task<dynamic> getById(int Id);
        Task<dynamic> createWorkerEvaluation(CreateWorkerEvaluationDetailsDtoRequest request);
        Task<dynamic> updateWorkerEvaluation(UpdateWorkerEvaluationDetailsDtoRequest request);
        Task<dynamic> deleteWorkerEvaluation(int Id);
    }
}
