using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IWorkerRepository
    {
        Task<List<WorkerDetail>> getAllAsync(int? jobId = null, int? jobOrderId = null, int? countryId = null);
        Task<List<WorkerDetail>> getAllByClientId(int? clientId = null, int? countryId = null);
        Task<WorkerDetail> getWorkerDetailsById(int workerId, int? countryId);
        Task<WorkerDetail> getWorkerDetails(int workerId, string workerCode, string workerName);
        Task<WorkerDetail> getWorkerFullDetailsById(int workerId, int? countryId);
        Task<WorkerDetail> getWorkerDetailsByJobOrderId(int jobOrderId);
        Task<List<WorkerStatusTypes>> getWorkerStatusTypeAsync();
        Task<List<WorkerContractStatusTypes>> getWorkerContractStatusTypeAsync();
        Task addWorkerDetails(WorkerDetail workerDetail);
        Task updateWorkerDetails(WorkerDetail workerDetail);
        Task deleteWorker(int workerId, string userCustomId);
    }
}
