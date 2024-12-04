using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly TapfinDbContext _dbContext;
        public CountryRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Country>> getAllAsync()
        {
            var countryList = await _dbContext.Country.AsQueryable().Where(w => w.IsActive == true)
                .Include(o=> o.Region)
                .ToListAsync();
            return countryList;
        }

        public async Task<Country> getCountryDetailsById(int countryId)
        {
            var countryDetails = await _dbContext.Country.AsQueryable().Where(w => w.IsActive == true && w.Id == countryId)
                .Include(o => o.Region)
                .FirstOrDefaultAsync();
            return countryDetails;
        }

        public async Task addCountry(Country country)
        {
            await _dbContext.Country.AddAsync(country);
        }

        public async Task updateCountry(Country country)
        {
            var countryData = _dbContext.Country.AsQueryable().Where(w => w.IsActive == true && w.Id == country.Id).FirstOrDefault();
            if (countryData != null)
            {
                countryData.RegionId = country.RegionId;
                countryData.CountryName = country.CountryName;
                countryData.CountryCode = country.CountryCode;
                countryData.Currency = country.Currency;
                countryData.UpdatedAt = DateTime.UtcNow;
                countryData.UpdatedBy = country.UpdatedBy;

                _dbContext.Entry(countryData).State = EntityState.Modified;
            }
        }

        public async Task deleteCountry(int Id, string usercustomId)
        {
            var country = _dbContext.Country.AsQueryable().Where(w => w.IsActive == true && w.Id == Id).FirstOrDefault();
            if (country != null)
            {
                country.IsActive = false;
                country.DeletedBy = usercustomId;
                country.DeletedAt = DateTime.Now;
            }
        }
    }
}
