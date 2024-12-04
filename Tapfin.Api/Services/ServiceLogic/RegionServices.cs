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
    public class RegionServices : IRegionServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public RegionServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAllRegion()
        {
            var regionList = await _uow.regionRepository.getAllAsync();
            dynamic regionListMapped = _mapper.Map<List<GetRegionDtoResponse>>(regionList);

            if (regionList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, regionListMapped, "All regions has been fetched successfully.");
        }

        public async Task<dynamic> getRegionById(int Id)
        {
            var region = await _uow.regionRepository.getRegionById(Id);
            dynamic regionMapped = _mapper.Map<GetRegionDtoResponse>(region);

            if (region == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, regionMapped, "Region data has been fetched successfully.");
        }

        public async Task<dynamic> createRegion(CreateRegionDtoRequest request)
        {
            var region = _mapper.Map<Region>(request);
            region.CreatedBy = _currentUser.UserCustomId;

            await _uow.regionRepository.addRegion(region);
            int status = (int)await _uow.SaveChangesAsync();

            dynamic createdRegionMapped = _mapper.Map<GetRegionDtoResponse>(region);

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Region is not created. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.Created, createdRegionMapped, "Region is created successfully.");
        }

        public async Task<dynamic> deleteRegion(int Id)
        {
            var userCustomId = _currentUser.UserCustomId;

            await _uow.regionRepository.deleteRegion(Id, userCustomId);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Region data is not deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Region is deleted successfully.");
        }

        public async Task<dynamic> updateRegion(UpdateRegionDtoRequest request)
        {
            var region = _mapper.Map<Region>(request);
            region.UpdatedBy = _currentUser.UserCustomId;

            await _uow.regionRepository.updateRegion(region);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Region data is not updated. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Region data is updated successfully.");
        }
    }
}
