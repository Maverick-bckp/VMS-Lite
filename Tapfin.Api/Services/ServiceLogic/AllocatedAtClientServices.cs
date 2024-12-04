using AutoMapper;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class AllocatedAtClientServices : IAllocatedAtClientServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public AllocatedAtClientServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAllTypes()
        {
            var allocatedTypesList = await _uow.allocatedAtClientRepository.getAllAsync();
            dynamic allocatedTypesListMapped = _mapper.Map<List<GetAllocatedAtClientTypeDtoResponse>>(allocatedTypesList);

            if (allocatedTypesList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, allocatedTypesListMapped, "All types has been fetched successfully.");
        }
    }
}
