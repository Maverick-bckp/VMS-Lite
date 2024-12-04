using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly TapfinDbContext _dbContext;
        public JobRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<JobDetail>> getAllAsync(int? countryId, int? departmentId, int? clientId)
        {
            var jobList = await _dbContext.JobDetails.AsQueryable().Where(w => w.IsActive == true && w.CountryId == countryId && (clientId == null || w.ClientId == clientId))
                        .Include(o => o.Client)
                        .Include(o => o.Country)
                        .ThenInclude(o => o.Region)
                        .Include(o => o.ServiceType)
                        .Include(o => o.AllocatedAtClient)
                        .Include(o => o.JobStatusType)
                        .Include(o => o.Department)
                        .ToListAsync();

            return jobList;
        }

        public async Task<JobDetail> getJobDetailsById(int jobId, int? countryId, int? departmentId, int? clientId)
        {
            var jobDetails = await _dbContext.JobDetails.AsQueryable().Where(w => w.IsActive == true && w.Id == jobId && w.CountryId == countryId && (clientId == null || w.ClientId == clientId))
                        .Include(o => o.Client)
                        .Include(o => o.Country)
                        .ThenInclude(o => o.Region)
                        .Include(o => o.ServiceType)
                        .Include(o => o.AllocatedAtClient)
                        .Include(o => o.JobStatusType)
                        .Include(o => o.Department)
                        .FirstOrDefaultAsync();
            return jobDetails;
        }

        public async Task<List<JobStatusType>> getJobStatusTypeAsync()
        {
            var jobStatusTypeList = await _dbContext.JobStatusType.AsQueryable().Where(w => w.IsActive == true).ToListAsync();
            return jobStatusTypeList;
        }

        public async Task addJobDetails(JobDetail jobDetail)
        {
            await _dbContext.JobDetails.AddAsync(jobDetail);
        }

        public async Task deleteJob(int jobId, string userCustomId)
        {
            var jobData = _dbContext.JobDetails.AsQueryable().Where(w => w.IsActive == true && w.Id == jobId).FirstOrDefault();
            if (jobData != null)
            {
                jobData.IsActive = false;
                jobData.DeletedAt = DateTime.Now;
                jobData.DeletedBy = userCustomId;
            }
        }

        public async Task updateJobDetails(JobDetail jobDetail)
        {
            var jobData = _dbContext.JobDetails.AsQueryable().Where(w => w.IsActive == true && w.Id == jobDetail.Id).FirstOrDefault();
            if (jobData != null)
            {
                jobData.ServiceTypeId = jobDetail.ServiceTypeId;
                jobData.DepartmentId = jobDetail.DepartmentId;
                jobData.AllocatedAtClientId = jobDetail.AllocatedAtClientId;
                jobData.JobCode = jobDetail.JobCode;
                jobData.JobTitle = jobDetail.JobTitle;
                jobData.JobDesc = jobDetail.JobDesc;
                jobData.JobLocation = jobDetail.JobLocation;
                jobData.NoOfPosition = jobDetail.NoOfPosition;
                jobData.YearsOfExperience = jobDetail.YearsOfExperience;
                jobData.ExpectedSalary = jobDetail.ExpectedSalary;
                jobData.Remarks = jobDetail.Remarks;
                jobData.JobStatus = jobDetail.JobStatus;
                jobData.UpdatedAt = DateTime.UtcNow;
                jobData.UpdatedBy = jobDetail.UpdatedBy;

                _dbContext.Entry(jobData).State = EntityState.Modified;
            }
        }
    }
}
