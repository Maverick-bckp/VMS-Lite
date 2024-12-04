using AutoMapper;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;
using static Tapfin.Api.Models.DTO.ClientRevenueDto;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class VendorBillingServices : IVendorBillingServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public VendorBillingServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAll(int vendorId)
        {
            var vendorBillingList = await _uow.VendorBillingRepository.getAllAsync(vendorId);
            dynamic vendorBillingListMapped = _mapper.Map<List<GetVendorBillingDtoResponse>>(vendorBillingList);

            if (vendorBillingList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, vendorBillingListMapped, "All vendor billing details has been fetched successfully.");
        }

        public async Task<dynamic> getVendorBillingById(int Id)
        {
            var vendorBilling = await _uow.VendorBillingRepository.getClientRevenueById(Id);
            dynamic vendorBillingMapped = _mapper.Map<GetVendorBillingDtoResponse>(vendorBilling);

            if (vendorBilling == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, vendorBillingMapped, "Vendor billing details has been fetched successfully.");
        }

        public async Task<dynamic> createVendorBilling(CreateVendorBillingDtoRequest request)
        {
            var vendorBilling = _mapper.Map<VendorBilling>(request);
            vendorBilling.CreatedBy = _currentUser.UserCustomId;

            await _uow.VendorBillingRepository.addVendorBilling(vendorBilling);
            int status = (int)await _uow.SaveChangesAsync();

            dynamic createdVendorBillingMapped = _mapper.Map<GetVendorBillingDtoResponse>(vendorBilling);

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Vendor billing details is not created. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.Created, message: "Vendor billing details is created successfully.");
        }

        public async Task<dynamic> updateVendorBillingDetails(UpdateVendorBillingDtoRequest request)
        {
            var vendorBilling = _mapper.Map<VendorBilling>(request);
            vendorBilling.UpdatedBy = _currentUser.UserCustomId;

            await _uow.VendorBillingRepository.updateVendorBillingDetails(vendorBilling);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Vendor billing details is not updated. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Vendor billing details is updated successfully.");
        }

        public async Task<dynamic> deleteVendorBilling(int Id)
        {
            var userCustomId = _currentUser.UserCustomId;

            await _uow.VendorBillingRepository.deleteVendorBilling(Id, userCustomId);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Vendor billing details is not deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Vendor billing details is deleted successfully.");
        }
    }
}
