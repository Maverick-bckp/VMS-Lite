using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface ICountryRepository
    {
        Task<List<Country>> getAllAsync();
        Task addCountry(Country country);
        Task updateCountry(Country country);
        Task deleteCountry(int Id, string usercustomId);
        Task<Country> getCountryDetailsById(int countryId);
    }
}
