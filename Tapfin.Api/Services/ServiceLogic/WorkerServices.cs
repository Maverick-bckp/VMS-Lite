using AutoMapper;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class WorkerServices : IWorkerServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public WorkerServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAll(int? jobId = null, int? jobOrderId = null)
        {
            int? countryId = _currentUser.CountryId;
            var workerList = await _uow.workerRepository.getAllAsync(jobId, jobOrderId, countryId);
            dynamic workerListMapped = _mapper.Map<List<GetWorkerDetailsDtoResponse>>(workerList);

            if (workerList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerListMapped, "All worker details has been fetched successfully.");
        }

        public async Task<dynamic> getWorkerDetailsById(int Id)
        {
            int? countryId = _currentUser.CountryId;
            var workerDetails = await _uow.workerRepository.getWorkerDetailsById(Id, countryId);
            dynamic workerDetailsMapped = _mapper.Map<GetWorkerDetailsDtoResponse>(workerDetails);

            if (workerDetails == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerDetailsMapped, "Worker data has been fetched successfully.");
        }

        public async Task<dynamic> getWorkerFullDetailsById(int Id)
        {
            int? countryId = _currentUser.CountryId;

            /*------- Get Worker Details And Set To DTO --------*/
            var workerFullDetails = await _uow.workerRepository.getWorkerFullDetailsById(Id, countryId);
            var workerFullDetailsMapped = _mapper.Map<GetWorkerFullDetailsDtoResponse>(workerFullDetails);


            /*--------- Set Hiring Manager details To DTO Response ---------*/
            if (workerFullDetails.HiringManagerId != null)
            {
                var hiringManagerDetails = await _uow.accountsRepository.GetUserDetailsByCustomId(workerFullDetails.HiringManagerId);
                workerFullDetailsMapped.HiringManagerDetails = _mapper.Map<GetWorkerFullDetailsHiringManagerDetailsDto>(hiringManagerDetails);
            }

            /*--------- Set Account Manager details To DTO Response ---------*/
            if (workerFullDetails.AccountManagerId != null)
            {
                var accountManagerDetails = await _uow.accountsRepository.GetUserDetailsByCustomId(workerFullDetails.AccountManagerId);
                workerFullDetailsMapped.AccountManagerDetails = _mapper.Map<GetWorkerFullDetailsAccountManagerDetailsDto>(accountManagerDetails);
            }

            if (workerFullDetails == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerFullDetailsMapped, "Worker details has been fetched successfully.");
        }

        public async Task<dynamic> getWorkerStatusType()
        {
            var workerStatusTypes = await _uow.workerRepository.getWorkerStatusTypeAsync();
            dynamic workerStatusTypesMapped = _mapper.Map<List<GetWorkerDetailsStatusType>>(workerStatusTypes);

            if (workerStatusTypes == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerStatusTypesMapped, "Worker status types has been fetched successfully.");
        }

        public async Task<dynamic> getWorkerContractStatusType()
        {
            var workerContractStatusTypes = await _uow.workerRepository.getWorkerContractStatusTypeAsync();
            dynamic workerContractStatusTypesMapped = _mapper.Map<List<GetWorkerDetailsContractStatusType>>(workerContractStatusTypes);

            if (workerContractStatusTypes == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerContractStatusTypesMapped, "Worker status types has been fetched successfully.");
        }

        public async Task<dynamic> createWorkerDetails(CreateWorkerDetailsDtoRequest request)
        {
            var workerDetails = _mapper.Map<WorkerDetail>(request);
            workerDetails.CountryId = _currentUser.CountryId;
            workerDetails.VendorId = _currentUser.VendorId;
            workerDetails.AccountManagerId = _currentUser.UserCustomId;
            workerDetails.CreatedBy = _currentUser.UserCustomId;

            await _uow.workerRepository.addWorkerDetails(workerDetails);
            int status = (int)await _uow.SaveChangesAsync();

            dynamic createdWorkerDetailsMapped = _mapper.Map<GetWorkerDetailsDtoResponse>(workerDetails);

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker details is not created. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.Created, message: "Worker details is created successfully.");
        }

        public async Task<dynamic> updateWorkerDetails(UpdateWorkerDetailsDtoRequest request)
        {
            var workerDetails = _mapper.Map<WorkerDetail>(request);
            workerDetails.UpdatedBy = _currentUser.UserCustomId;

            await _uow.workerRepository.updateWorkerDetails(workerDetails);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker details is not updated. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Worker details is updated successfully.");
        }

        public async Task<dynamic> deleteWorkerDetails(int Id)
        {
            var userCustomId = _currentUser.UserCustomId;

            await _uow.workerRepository.deleteWorker(Id, userCustomId);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker details is not deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Worker details is deleted successfully.");
        }
    }
}
