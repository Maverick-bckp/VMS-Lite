using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IWorkerEquipmentCostRepository
    {
        Task<List<WorkerEquipmentCost>> getAllAsync(int workerId);
        Task<WorkerEquipmentCost> getWorkerEquipmentCostDetailsById(int id);
        Task addWorkerEquipmentCostDetails(WorkerEquipmentCost workerEquipmentCost);
        Task updateWorkerEquipmentCostDetails(WorkerEquipmentCost workerEquipmentCost);
        Task deleteWorkerEquipmentCost(int Id, string userCustomId);
    }
}
