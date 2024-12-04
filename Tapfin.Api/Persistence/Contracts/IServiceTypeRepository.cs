using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IServiceTypeRepository
    {
        Task<List<ServiceType>> getAllAsync();
    }
}
