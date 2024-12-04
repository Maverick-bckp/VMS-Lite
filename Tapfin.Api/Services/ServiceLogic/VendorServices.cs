using AutoMapper;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class VendorServices : IVendorServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public VendorServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAllVendor()
        {
            int? countryId = _currentUser.CountryId;

            var vendorList = await _uow.vendorRepository.getAllAsync(countryId);

            if (vendorList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, message: "No Data Found.");
            }


            /*------ Map vendor Data To DTO ----*/
            var vendorListMapped = _mapper.Map<List<GetVendorDetailsDtoResponse>>(vendorList);

            /*-------- Map Region data in DTO --------*/
            foreach (var vendor in vendorList)
            {
                var vendorMapped = vendorListMapped.FirstOrDefault(i => i.Id == vendor.Id);
                vendorMapped.Region = _mapper.Map<VendorDetailsRegionDto>(vendor.Country.Region);
            }

            /*-------- Map User data in DTO --------*/
            foreach (var vendor in vendorList)
            {
                var vendorMapped = vendorListMapped.FirstOrDefault(i => i.Id == vendor.Id);
                vendorMapped.User = _mapper.Map<List<GetVendorUsersDtoResponse>>(vendor.AspNetUsers);
            }


            return _result.AddReturnData(HttpStatusCode.OK, vendorListMapped, "All vendor data has been fetched successfully.");
        }

        public async Task<dynamic> getVendorById(int Id)
        {
            var countryId = _currentUser.CountryId;

            var vendorData = await _uow.vendorRepository.getVendorDetailsById(Id, countryId);

            if (vendorData == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, message: "No Data Found.");
            }


            dynamic vendorListMapped = _mapper.Map<GetVendorDetailsDtoResponse>(vendorData);
            vendorListMapped.Region = _mapper.Map<VendorDetailsRegionDto>(vendorData.Country.Region);
            vendorListMapped.User = _mapper.Map<List<GetVendorUsersDtoResponse>>(vendorData.AspNetUsers);

            return _result.AddReturnData(HttpStatusCode.OK, vendorListMapped, "Vendor data has been fetched successfully.");
        }

        public async Task<dynamic> createVendor(CreateVendorDtoRequest request)
        {

            /*------- Begin Transaction -------*/
            await _uow.BeginTransactionAsync();

            try
            {
                /*--------- Create Vendor Details ----------*/
                var vendorData = _mapper.Map<VendorDetail>(request);
                vendorData.CountryId = request.CountryId;
                vendorData.CreatedBy = _currentUser.UserCustomId;
                await _uow.vendorRepository.addVendor(vendorData);
                int status = (int)await _uow.SaveChangesAsync();

                /*--------- Create Vendor Address ----------*/
                var vendorAddressData = _mapper.Map<List<VendorAddress>>(request.Address);
                foreach (var vendorAddress in vendorAddressData)
                {
                    vendorAddress.VendorId = vendorData.Id;
                    vendorAddress.CreatedBy = _currentUser.UserCustomId;
                    await _uow.VendorAddressRepository.addVendorAddress(vendorAddress);
                }
                int addVendorAddressStatus = (int)await _uow.SaveChangesAsync();


                /*------- Commit Transaction -------*/
                await _uow.CommitAsync();


                /*------ Return Response -------*/
                if (status < 1)
                {
                    return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Vendor is not created. Please try again.");
                }

                return _result.AddReturnData(HttpStatusCode.Created, message: "Vendor is created successfully.");

            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return _result.AddReturnData(HttpStatusCode.OK, message: ex.Message.ToString());
            }
        }

        public async Task<dynamic> updateVendor(UpdateVendorDetailsDtoRequest request)
        {
            /*------- Begin Transaction -------*/
            await _uow.BeginTransactionAsync();

            try
            {
                /*----------- Update Client Details-----------*/
                var vendorData = _mapper.Map<VendorDetail>(request);
                vendorData.UpdatedBy = _currentUser.UserCustomId;
                await _uow.vendorRepository.updateVendorDetails(vendorData);
                int status = (int)await _uow.SaveChangesAsync();

                if (status < 1)
                {
                    return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Vendor data not updated. Please try again.");
                }


                /*--------- Create/Update Client Address ----------*/
                if (request.Address.Count > 0)
                {
                    foreach (var vendorAddress in request.Address)
                    {
                        var vendorAddressData = _mapper.Map<VendorAddress>(vendorAddress);
                        var dbStatus = await VendorAddressInsertOrUpdate(vendorAddressData, vendorData.Id);

                        if (dbStatus < 1)
                        {
                            return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Vendor data is not updated. Please check details and try again.");
                        }
                    }
                }


                /*------- Commit Transaction -------*/
                await _uow.CommitAsync();

                return _result.AddReturnData(HttpStatusCode.OK, message: "Vendor details is updated successfully.");
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return _result.AddReturnData(HttpStatusCode.OK, message: ex.Message.ToString());
            }
        }

        public async Task<dynamic> deleteVendor(int Id)
        {
            await _uow.vendorRepository.deleteVendor(Id);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Vendor data is not deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Vendor is deleted successfully.");
        }

        public async Task<dynamic> deleteVendorAddress(int id)
        {
            var userCustomId = _currentUser.UserCustomId;

            await _uow.VendorAddressRepository.deleteVendorAddress(id, userCustomId);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Vendor address data is not deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Vendor address data is deleted successfully.");
        }

        private async Task<int> VendorAddressInsertOrUpdate(VendorAddress vendorAddress, int vendorId)
        {
            int status;
            var vendorAddressData = await _uow.VendorAddressRepository.getVendorAddressById(vendorAddress.Id);


            if (vendorAddressData == null) /*--- Insert Client Address ---*/
            {
                vendorAddress.VendorId = vendorId;
                vendorAddress.CreatedBy = _currentUser.UserCustomId;
                await _uow.VendorAddressRepository.addVendorAddress(vendorAddress);
                status = (int)await _uow.SaveChangesAsync();
            }
            else                           /*--- Update Client Address ---*/
            {
                vendorAddress.UpdatedBy = _currentUser.UserCustomId;
                await _uow.VendorAddressRepository.updateVendorAddress(vendorAddress);
                status = (int)await _uow.SaveChangesAsync();
            }

            return status;
        }
    }
}
