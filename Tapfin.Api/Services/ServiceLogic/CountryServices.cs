using AutoMapper;
using Newtonsoft.Json;
using System.Net;
using Tapfin.Api.Enums;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class CountryServices : ICountryServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public CountryServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper; 
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAllCountry()
        {
            var countryList = await _uow.countryRepository.getAllAsync();
            dynamic countryListMapped = _mapper.Map<List<GetCountryDtoResponse>>(countryList);

            if (countryList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, message: "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, countryListMapped, "All countries data fetched successfully.");
        }

        public async Task<dynamic> getCountryById(int Id)
        {
            var country = await _uow.countryRepository.getCountryDetailsById(Id);
            dynamic countryMapped = _mapper.Map<GetCountryDtoResponse>(country);

            if (country == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, message: "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, countryMapped, "Country data fetched successfully.");
        }

        public async Task<dynamic> createCountry(CreateCountryDtoRequest request)
        {
            var country = _mapper.Map<Country>(request);
            country.CreatedBy = _currentUser.UserCustomId;

            await _uow.countryRepository.addCountry(country);
            int status = (int)await _uow.SaveChangesAsync();

            dynamic createdCountryMapped = _mapper.Map<GetCountryDtoResponse>(country);

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Country is not created. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.Created, "Country details is created successfully.");
        }


        public async Task<dynamic> updateCountry(UpdateCountryDtoRequest request)
        {
            var country = _mapper.Map<Country>(request);
            country.UpdatedBy = _currentUser.UserCustomId;

            await _uow.countryRepository.updateCountry(country);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Country data cannnot be updated. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Country data is updated successfully.");
        }

        public async Task<dynamic> deleteCountry(int Id)
        {
            var usercustomId = _currentUser.UserCustomId;
            await _uow.countryRepository.deleteCountry(Id, usercustomId!);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Country data cannnot be deleted. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Country data is deleted successfully.");
        }

        public async Task<dynamic> getCurrency()
        {
            List<string> currencyNames = new List<string>();

            var currencyList = Enum.GetValues(typeof(Currency)).Cast<Currency>().ToList();

            foreach (var currency in currencyList)
            {
                currencyNames.Add(currency.ToString());
            }

            return _result.AddReturnData(HttpStatusCode.OK, currencyNames, message: "Currency data has been fetched successfully.");
        }
    }
}
