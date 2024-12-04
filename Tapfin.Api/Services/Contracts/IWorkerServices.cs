using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Services.Contracts
{
    public interface IWorkerServices
    {
        Task<dynamic> getAll(int? jobId = null, int? jobOrderId = null);
        Task<dynamic> getWorkerDetailsById(int Id);
        Task<dynamic> getWorkerFullDetailsById(int Id);
        Task<dynamic> getWorkerStatusType();
        Task<dynamic> getWorkerContractStatusType();
        Task<dynamic> createWorkerDetails(CreateWorkerDetailsDtoRequest request);
        Task<dynamic> updateWorkerDetails(UpdateWorkerDetailsDtoRequest request);
        Task<dynamic> deleteWorkerDetails(int Id);
    }
}
