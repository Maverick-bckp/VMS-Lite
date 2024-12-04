using AutoMapper;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class JobServices : IJobServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public JobServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAllJobs()
        {
            int? countryId = _currentUser.CountryId;
            var departmentId = _currentUser?.DepartmentId;
            var clientId = _currentUser?.ClientId;

            var jobsList = await _uow.jobRepository.getAllAsync(countryId, departmentId, clientId);

            if (jobsList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, message: "No Data Found.");
            }


            /*-------- Map Job details to DTO -------*/
            var jobsListMapped = _mapper.Map<List<GetJobDetailsDtoResponse>>(jobsList);
            foreach (var job in jobsList)
            {
                var jobMapped = jobsListMapped.FirstOrDefault(i => i.Id == job.Id);
                jobMapped.Region = _mapper.Map<JobDetailsRegionDto>(job.Country.Region);
            }


            return _result.AddReturnData(HttpStatusCode.OK, jobsListMapped, "All jobs has been fetched successfully.");
        }

        public async Task<dynamic> getJobDetailsById(int Id)
        {
            int? countryId = _currentUser.CountryId;
            var departmentId = _currentUser?.DepartmentId;
            var clientId = _currentUser?.ClientId;

            var jobsData = await _uow.jobRepository.getJobDetailsById(Id, countryId, departmentId, clientId);
            if (jobsData == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, message: "No Data Found.");
            }


            dynamic jobsDataMapped = _mapper.Map<GetJobDetailsDtoResponse>(jobsData);
            jobsDataMapped.Region = _mapper.Map<JobDetailsRegionDto>(jobsData.Country.Region);

            return _result.AddReturnData(HttpStatusCode.OK, jobsDataMapped, "Job details has been fetched successfully.");
        }

        public async Task<dynamic> getAllJobStatusTypes()
        {
            var jobStatustypesList = await _uow.jobRepository.getJobStatusTypeAsync();
            var jobStatustypesListMapped = _mapper.Map<List<GetJobStatusTypesDtoResponse>>(jobStatustypesList);

            if (jobStatustypesList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, jobStatustypesListMapped, "All job status types has been fetched successfully.");
        }

        public async Task<dynamic> createJob(CreateJobDtoRequest request)
        {
            var jobData = _mapper.Map<JobDetail>(request);
            jobData.ClientId = Convert.ToInt32(_currentUser.ClientId);
            jobData.CountryId = Convert.ToInt32(_currentUser.CountryId);
            jobData.CreatedBy = _currentUser.UserCustomId;

            await _uow.jobRepository.addJobDetails(jobData);
            int status = (int)await _uow.SaveChangesAsync();

            //dynamic createdJobMapped = getJobDetailsById(jobData.Id);

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Job is not created. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.Created, message: "Job is created successfully.");
        }

        public async Task<dynamic> updateJob(UpdateJobDetailsDtoRequest request)
        {
            var jobData = _mapper.Map<JobDetail>(request);
            jobData.UpdatedAt = DateTime.UtcNow;
            jobData.UpdatedBy = _currentUser.UserCustomId;

            await _uow.jobRepository.updateJobDetails(jobData);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Job details is not updated. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Job details is updated successfully.");
        }

        public async Task<dynamic> deleteJob(int Id)
        {
            var userCustomId = _currentUser.UserCustomId;

            await _uow.jobRepository.deleteJob(Id, userCustomId);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Job data is not deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Job is deleted successfully.");
        }
    }
}
