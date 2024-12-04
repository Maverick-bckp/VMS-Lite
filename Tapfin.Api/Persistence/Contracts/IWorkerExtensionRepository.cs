using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IWorkerExtensionRepository
    {
        Task<List<WorkerExtension>> getAllAsync(int workerId);
        Task<List<WorkerExtensionStatusTypes>> getAllStatusTypesAsync();
        Task<WorkerExtension> getWorkerExtensionDetailsById(int id);
        Task addWorkerExtensionDetails(WorkerExtension workerExtension);
        Task updateWorkerExtensionDetails(WorkerExtension workerExtension);
        Task deleteWorkerExtension(int Id, string userCustomId);
    }
}
