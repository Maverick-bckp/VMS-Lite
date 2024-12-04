using AutoMapper;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class JobOrderServices : IJobOrderServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public JobOrderServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAllJobOrders(int jobId)
        {
            var jobOrdersList = await _uow.jobOrderRepository.getAllAsync(jobId);

            if (jobOrdersList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }



            var jobOrdersListMapped = _mapper.Map<List<GetJobOrdersDtoResponse>>(jobOrdersList);

            /*-------- Map User data in DTO --------*/
            foreach (var jobOrder in jobOrdersList)
            {
                var jobOrdersMapped = jobOrdersListMapped.FirstOrDefault(i => i.Id == jobOrder.Id);

                /*------------- Get User Details and Set value to JSON Response Property -------------*/
                var userDetails = await _accountsServices.GetUserDetailsByCustomId(jobOrdersMapped.CreatedBy);
                jobOrdersMapped.User = _mapper.Map<UserBasicdetailsDto>(userDetails);


                /*-------- Check If Worker Available and Set Value to JSON Response Property ---------*/
                var workerDetails = await _uow.workerRepository.getWorkerDetailsByJobOrderId(jobOrder.Id);
                jobOrdersMapped.IsWorkerAvailable = workerDetails != null ? true : false;
            }


            return _result.AddReturnData(HttpStatusCode.OK, jobOrdersListMapped, "All job orders has been fetched successfully.");
        }

        public async Task<dynamic> getAllJobOrderStatusTypes()
        {
            var jobOrderStatustypesList = await _uow.jobOrderRepository.getJobOrderStatusTypeAsync();
            dynamic jobOrderStatustypesListMapped = _mapper.Map<List<GetJobOrdersStatusTypesDtoResponse>>(jobOrderStatustypesList);

            if (jobOrderStatustypesList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, jobOrderStatustypesListMapped, "All job order status types has been fetched successfully.");
        }

        public async Task<dynamic> getJobOrderById(int Id)
        {
            var jobOrderData = await _uow.jobOrderRepository.getJobOrderDetailsById(Id);

            if (jobOrderData == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }


            var jobOrderMapped = _mapper.Map<GetJobOrdersDtoResponse>(jobOrderData);
            var userDetails = await _accountsServices.GetUserDetailsByCustomId(jobOrderData.CreatedBy);
            jobOrderMapped.User = _mapper.Map<UserBasicdetailsDto>(userDetails);


            return _result.AddReturnData(HttpStatusCode.OK, jobOrderMapped, "Job order data has been fetched successfully.");
        }

        public async Task<dynamic> createJobOrder(CreateJobOrderDtoRequest request)
        {
            var jobOrderData = _mapper.Map<JobOrder>(request);
            jobOrderData.CreatedBy = _currentUser.UserCustomId;


            /*--------- Validate Required Positions in Job Requisition ----------*/
            var jobDetails = await _uow.jobRepository.getJobDetailsById(request.JobId, _currentUser.CountryId, _currentUser?.DepartmentId, _currentUser?.ClientId);
            var jobOrdersList = await _uow.jobOrderRepository.getAllAsync(request.JobId);
            if (jobOrdersList.Count >= jobDetails.NoOfPosition)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Job order couldn't be created. The request exceeds required no. of positions.");
            }


            /*-------- Create Job Order ---------*/
            await _uow.jobOrderRepository.addJobOrder(jobOrderData);
            int status = (int)await _uow.SaveChangesAsync();

            dynamic createdJobOrderMapped = _mapper.Map<GetJobOrdersDtoResponse>(jobOrderData);

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Job order is not created. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.Created, message: "job order details is created successfully.");
        }

        public async Task<dynamic> updateJobOrder(UpdateJobOrderDtoRequest request)
        {
            var jobOrderData = _mapper.Map<JobOrder>(request);
            jobOrderData.UpdatedBy = _currentUser.UserCustomId;

            await _uow.jobOrderRepository.updateJobOrderDetails(jobOrderData);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Job order details is not updated. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Job order data is updated successfully.");
        }

        public async Task<dynamic> deleteJobOrder(int Id)
        {
            var userCustomId = _currentUser.UserCustomId;

            await _uow.jobOrderRepository.deleteJobOrder(Id, userCustomId);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Job order is not deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Job order is deleted successfully.");
        }
    }
}
