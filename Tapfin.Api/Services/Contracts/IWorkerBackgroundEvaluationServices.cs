using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Services.Contracts
{
    public interface IWorkerBackgroundEvaluationServices
    {
        Task<dynamic> getAll(int workerId);
        Task<dynamic> getAllValidationTypes();
        Task<dynamic> getById(int Id);
        Task<dynamic> createWorkerBckgEvaluation(CreateWorkerBckgEvaluationDetailsDtoRequest request);
        Task<dynamic> updateWorkerBckgEvaluation(UpdateWorkerBckgEvaluationDetailsDtoRequest request);
        Task<dynamic> deleteWorkerBckgEvaluation(int Id);
    }
}
