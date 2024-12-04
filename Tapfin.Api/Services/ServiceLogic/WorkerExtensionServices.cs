using AutoMapper;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class WorkerExtensionServices : IWorkerExtensionServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public WorkerExtensionServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAll(int workerId)
        {
            var workerExtensionList = await _uow.workerExtensionRepository.getAllAsync(workerId);
            dynamic workerExtensionListMapped = _mapper.Map<List<GetWorkerExtensionDetailsDtoResponse>>(workerExtensionList);

            if (workerExtensionList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerExtensionListMapped, "All details has been fetched successfully.");
        }

        public async Task<dynamic> getAllExtensionStatusTypes()
        {
            var workerExtensionStatusTypesList = await _uow.workerExtensionRepository.getAllStatusTypesAsync();
            dynamic workerExtensionStatusTypesListMapped = _mapper.Map<List<GeWorkerExtensionDetailsStatusTypesDtoResponse>>(workerExtensionStatusTypesList);

            if (workerExtensionStatusTypesList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerExtensionStatusTypesListMapped, "All details has been fetched successfully.");
        }

        public async Task<dynamic> getById(int Id)
        {
            var workerExtensionDetails = await _uow.workerExtensionRepository.getWorkerExtensionDetailsById(Id);
            dynamic workerExtensionDetailsMapped = _mapper.Map<GetWorkerExtensionDetailsDtoResponse>(workerExtensionDetails);

            if (workerExtensionDetails == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerExtensionDetailsMapped, "Worker extension data has been fetched successfully.");
        }

        public async Task<dynamic> createWorkerExtension(CreateWorkerExtensionDetailsDtoRequest request)
        {
            var workerExtensionDetails = _mapper.Map<WorkerExtension>(request);
            workerExtensionDetails.CreatedBy = _currentUser.UserCustomId;

            await _uow.workerExtensionRepository.addWorkerExtensionDetails(workerExtensionDetails);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker extension details is not created. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.Created, message: "Worker extension details is created successfully.");
        }

        public async Task<dynamic> updateWorkerExtension(UpdateWorkerExtensionDetailsDtoRequest request)
        {
            var workerExtension = _mapper.Map<WorkerExtension>(request);
            workerExtension.UpdatedBy = _currentUser.UserCustomId;

            await _uow.workerExtensionRepository.updateWorkerExtensionDetails(workerExtension);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker extension details is not updated. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Worker extension details is updated successfully.");
        }

        public async Task<dynamic> deleteWorkerExtension(int Id)
        {
            var userCustomId = _currentUser.UserCustomId;

            await _uow.workerExtensionRepository.deleteWorkerExtension(Id, userCustomId);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker extension data is not deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Worker extension data is deleted successfully.");
        }
    }
}
