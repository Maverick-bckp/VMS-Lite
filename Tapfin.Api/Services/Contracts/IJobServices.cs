using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Services.Contracts
{
    public interface IJobServices
    {
        Task<dynamic> getAllJobs();
        Task<dynamic> getJobDetailsById(int Id);
        Task<dynamic> getAllJobStatusTypes();
        Task<dynamic> createJob(CreateJobDtoRequest request);
        Task<dynamic> updateJob(UpdateJobDetailsDtoRequest request);
        Task<dynamic> deleteJob(int Id);
    }
}
