using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IJobOrderRepository
    {
        Task<List<JobOrder>> getAllAsync(int jobId);
        Task<List<JobOrderStatusType>> getJobOrderStatusTypeAsync();
        Task<JobOrder> getJobOrderDetailsById(int jobOrderId);
        Task addJobOrder(JobOrder jobOrder);
        Task updateJobOrderDetails(JobOrder jobOrder);
        Task deleteJobOrder(int Id, string userCustomId);
    }
}
