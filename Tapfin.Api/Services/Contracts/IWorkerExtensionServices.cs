using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Services.Contracts
{
    public interface IWorkerExtensionServices
    {
        Task<dynamic> getAll(int workerId);
        Task<dynamic> getById(int Id);
        Task<dynamic> getAllExtensionStatusTypes();
        Task<dynamic> createWorkerExtension(CreateWorkerExtensionDetailsDtoRequest request);
        Task<dynamic> updateWorkerExtension(UpdateWorkerExtensionDetailsDtoRequest request);
        Task<dynamic> deleteWorkerExtension(int Id);
    }
}
