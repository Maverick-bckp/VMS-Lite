using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Services.Contracts
{
    public interface IWorkerEquipmentCostServices
    {
        Task<dynamic> getAll(int workerId);
        Task<dynamic> getById(int Id);
        Task<dynamic> createWorkerEquipmentCost(CreateWorkerEquipmentCostDetailsDtoRequest request);
        Task<dynamic> updateWorkerEquipmentCost(UpdateWorkerEquipmentCostDetailsDtoRequest request);
        Task<dynamic> deleteWorkerEquipmentCost(int Id);
    }
}
