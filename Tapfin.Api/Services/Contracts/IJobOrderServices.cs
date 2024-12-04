using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Services.Contracts
{
    public interface IJobOrderServices
    {
        Task<dynamic> getAllJobOrders(int jobId);
        Task<dynamic> getAllJobOrderStatusTypes();
        Task<dynamic> getJobOrderById(int Id);
        Task<dynamic> createJobOrder(CreateJobOrderDtoRequest request);
        Task<dynamic> updateJobOrder(UpdateJobOrderDtoRequest request);
        Task<dynamic> deleteJobOrder(int Id);
    }
}
