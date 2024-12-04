using AutoMapper;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class WorkerEvaluationServices : IWorkerEvaluationServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public WorkerEvaluationServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAll(int workerId)
        {
            var workerEvaluationList = await _uow.WorkerEvaluationRepository.getAllAsync(workerId);
            dynamic workerEvaluationListMapped = _mapper.Map<List<GetWorkerEvaluationDetailsDtoResponse>>(workerEvaluationList);

            if (workerEvaluationList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerEvaluationListMapped, "All details has been fetched successfully.");
        }

        public async Task<dynamic> getById(int Id)
        {
            var workerEvaluationDetails = await _uow.WorkerEvaluationRepository.getWorkerEvaluationDetailsById(Id);
            dynamic workerEvaluationDetailsMapped = _mapper.Map<GetWorkerEvaluationDetailsDtoResponse>(workerEvaluationDetails);

            if (workerEvaluationDetails == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerEvaluationDetailsMapped, "Worker evaluation data has been fetched successfully.");
        }

        public async Task<dynamic> createWorkerEvaluation(CreateWorkerEvaluationDetailsDtoRequest request)
        {
            var workerEvaluationDetails = _mapper.Map<WorkerEvaluation>(request);
            workerEvaluationDetails.CreatedBy = _currentUser.UserCustomId;

            await _uow.WorkerEvaluationRepository.addWorkerEvaluationDetails(workerEvaluationDetails);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker evaluation details is not created. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.Created, message: "Worker evaluation details is created successfully.");
        }

        public async Task<dynamic> updateWorkerEvaluation(UpdateWorkerEvaluationDetailsDtoRequest request)
        {
            var workerEvaluation = _mapper.Map<WorkerEvaluation>(request);
            workerEvaluation.UpdatedBy = _currentUser.UserCustomId;

            await _uow.WorkerEvaluationRepository.updateWorkerEvaluationDetails(workerEvaluation);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker evaluation details is not updated. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Worker evaluation details is updated successfully.");
        }

        public async Task<dynamic> deleteWorkerEvaluation(int Id)
        {
            var userCustomId = _currentUser.UserCustomId;

            await _uow.WorkerEvaluationRepository.deleteWorkerEvaluation(Id, userCustomId);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker evaluation data is not deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Worker evaluation data is deleted successfully.");
        }
    }
}
