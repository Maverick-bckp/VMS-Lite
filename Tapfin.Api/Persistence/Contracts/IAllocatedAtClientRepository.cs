using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IAllocatedAtClientRepository
    {
        Task<List<AllocatedAtClient>> getAllAsync();
    }
}
