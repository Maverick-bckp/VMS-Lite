using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Services.Contracts
{
    public interface ICountryServices
    {
        Task<dynamic> getAllCountry();
        Task<dynamic> getCountryById(int Id);
        Task<dynamic> createCountry(CreateCountryDtoRequest request);
        Task<dynamic> updateCountry(UpdateCountryDtoRequest request);
        Task<dynamic> deleteCountry(int Id);
        Task<dynamic> getCurrency();
    }
}
