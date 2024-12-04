using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Services.Contracts
{
    public interface IWorkerBIServices
    {
        Task<dynamic> getAllBIData(WorkerBIDetailsRequestDto request);
    }
}
