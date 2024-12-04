using AutoMapper;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class WorkerBackgroundEvaluationServices : IWorkerBackgroundEvaluationServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public WorkerBackgroundEvaluationServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAll(int workerId)
        {
            var workerBckgEvaluationList = await _uow.workerBackgroundEvaluationRepository.getAllAsync(workerId);
            dynamic workerBckgEvaluationListMapped = _mapper.Map<List<GetWorkerBckgEvaluationDetailsDtoResponse>>(workerBckgEvaluationList);

            if (workerBckgEvaluationList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerBckgEvaluationListMapped, "All details has been fetched successfully.");
        }

        public async Task<dynamic> getAllValidationTypes()
        {
            var workerBckgEvaluationValidationTypesList = await _uow.workerBackgroundEvaluationRepository.getAllValidationTypesAsync();
            dynamic workerBckgEvaluationValidationTypesListMapped = _mapper.Map<List<GetWorkerBckgEvaluationValidationTypesDtoResponse>>(workerBckgEvaluationValidationTypesList);

            if (workerBckgEvaluationValidationTypesList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerBckgEvaluationValidationTypesListMapped, "All details has been fetched successfully.");
        }

        public async Task<dynamic> getById(int Id)
        {
            var workerBckgEvaluationDetails = await _uow.workerBackgroundEvaluationRepository.getWorkerBckgEvaluationDetailsById(Id);
            dynamic workerBckgEvaluationDetailsMapped = _mapper.Map<GetWorkerBckgEvaluationDetailsDtoResponse>(workerBckgEvaluationDetails);

            if (workerBckgEvaluationDetails == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerBckgEvaluationDetailsMapped, "Worker background evaluation data has been fetched successfully.");
        }

        public async Task<dynamic> createWorkerBckgEvaluation(CreateWorkerBckgEvaluationDetailsDtoRequest request)
        {
            var workerBckgEvaluationDetails = _mapper.Map<WorkerBackgroundEvaluation>(request);
            workerBckgEvaluationDetails.CreatedBy = _currentUser.UserCustomId;

            await _uow.workerBackgroundEvaluationRepository.addWorkerBckgEvaluationDetails(workerBckgEvaluationDetails);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker background evaluation details is not created. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.Created, message: "Worker background evaluation details is created successfully.");
        }

        public async Task<dynamic> updateWorkerBckgEvaluation(UpdateWorkerBckgEvaluationDetailsDtoRequest request)
        {
            var workerBckgEvaluation = _mapper.Map<WorkerBackgroundEvaluation>(request);
            workerBckgEvaluation.UpdatedBy = _currentUser.UserCustomId;

            await _uow.workerBackgroundEvaluationRepository.updateWorkerBckgEvaluationDetails(workerBckgEvaluation);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker background evaluation details is not updated. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Worker background evaluation details is updated successfully.");
        }

        public async Task<dynamic> deleteWorkerBckgEvaluation(int Id)
        {
            var userCustomId = _currentUser.UserCustomId;

            await _uow.workerBackgroundEvaluationRepository.deleteWorkerBckgEvaluation(Id, userCustomId);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker background evaluation data is not deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Worker background evaluation data is deleted successfully.");
        }
    }
}
