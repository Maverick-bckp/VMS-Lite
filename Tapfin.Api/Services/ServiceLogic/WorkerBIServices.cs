using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class WorkerBIServices : IWorkerBIServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public WorkerBIServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAllBIData(WorkerBIDetailsRequestDto request)
        {
            dynamic dashboardJSONResponse = new JObject();


            /*-------- Get Worker Count Per Department from Stored Procedure --------*/
            var workerCountPerDepartmentList = await _uow.workerBIRepository.getWorkerCountPerDepartment(request.CountryId, request.ClientId, request.VendorId);
            dynamic workerCountPerDepartmentListMapped = _mapper.Map<List<GetWorkerCountPerDepartmentSPDto>>(workerCountPerDepartmentList);




            /*-------- Create final JSON Response --------*/
            dashboardJSONResponse["WorkerCountPerDepartment"] = JArray.Parse(JsonConvert.SerializeObject(workerCountPerDepartmentListMapped));

            return _result.AddReturnData(HttpStatusCode.OK, dashboardJSONResponse, "All dashboard data fetched successfully.");
        }
    }
}
