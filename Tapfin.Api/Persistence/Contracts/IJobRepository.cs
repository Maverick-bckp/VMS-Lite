using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IJobRepository
    {
        Task<List<JobDetail>> getAllAsync(int? countryId, int? departmentId, int? clientId);
        Task<List<JobStatusType>> getJobStatusTypeAsync();
        Task addJobDetails(JobDetail jobDetail);
        Task updateJobDetails(JobDetail jobDetail);
        Task deleteJob(int jobId, string userCustomId);
        Task<JobDetail> getJobDetailsById(int jobId, int? countryId, int? departmentId, int? clientId);
    }
}
