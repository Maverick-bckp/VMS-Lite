using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.ViewModels;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IWorkerBIRepository
    {
        Task<List<GetWorkerCountPerDepartment>> getWorkerCountPerDepartment(int? countryId, int? clientId, int? vendorId);
    }
}
