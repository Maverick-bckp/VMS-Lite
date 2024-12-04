using AutoMapper;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Persistence.Repositories;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class WorkerEquipmentCostServices : IWorkerEquipmentCostServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public WorkerEquipmentCostServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAll(int workerId)
        {
            var workerEquipmentCostList = await _uow.WorkerEquipmentCostRepository.getAllAsync(workerId);
            dynamic workerEquipmentCostListMapped = _mapper.Map<List<GetWorkerEquipmentCostDetailsDtoResponse>>(workerEquipmentCostList);

            if (workerEquipmentCostList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerEquipmentCostListMapped, "All details has been fetched successfully.");
        }

        public async Task<dynamic> getById(int Id)
        {
            var workerEquipmentCost = await _uow.WorkerEquipmentCostRepository.getWorkerEquipmentCostDetailsById(Id);
            dynamic workerEquipmentCostMapped = _mapper.Map<GetWorkerEquipmentCostDetailsDtoResponse>(workerEquipmentCost);

            if (workerEquipmentCost == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, workerEquipmentCostMapped, "Worker equipment cost data has been fetched successfully.");
        }

        public async Task<dynamic> createWorkerEquipmentCost(CreateWorkerEquipmentCostDetailsDtoRequest request)
        {
            var workerEquipmentCost = _mapper.Map<WorkerEquipmentCost>(request);
            workerEquipmentCost.CreatedBy = _currentUser.UserCustomId;

            await _uow.WorkerEquipmentCostRepository.addWorkerEquipmentCostDetails(workerEquipmentCost);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker equipment cost details is not created. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.Created,message: "Worker equipment cost details is created successfully.");
        }

        public async Task<dynamic> updateWorkerEquipmentCost(UpdateWorkerEquipmentCostDetailsDtoRequest request)
        {
            var workerEquipmentCost = _mapper.Map<WorkerEquipmentCost>(request);
            workerEquipmentCost.UpdatedBy = _currentUser.UserCustomId;

            await _uow.WorkerEquipmentCostRepository.updateWorkerEquipmentCostDetails(workerEquipmentCost);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker equipment cost details is not updated. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Worker equipment cost details is updated successfully.");
        }

        public async Task<dynamic> deleteWorkerEquipmentCost(int Id)
        {
            var userCustomId = _currentUser.UserCustomId;

            await _uow.WorkerEquipmentCostRepository.deleteWorkerEquipmentCost(Id, userCustomId);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Worker equipment cost data is not deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Worker equipment cost data is deleted successfully.");
        }
    }
}
