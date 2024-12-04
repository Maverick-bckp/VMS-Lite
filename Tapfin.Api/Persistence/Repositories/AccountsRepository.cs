using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using Tapfin.Api.Models;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly TapfinDbContext _dbContext;
        public AccountsRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UpdateUserCustomId(int sid, string customId)
        {
            var user = _dbContext.AspNetUsers.AsQueryable().Where(w => w.IsActive == true && w.SId == sid).FirstOrDefault();
            if (user != null)
            {
                user.UserCustomId = customId;

                _dbContext.Entry(user).State = EntityState.Modified;
            }
        }

        public User GetUserDetails(string emailId)
        {
            var user = _dbContext.AspNetUsers.AsQueryable().Where(w => w.IsActive == true && w.Email == emailId)
                .Include(o => o.Country)
                .ThenInclude(o => o.Region)
                .Include(o => o.Client)
                .Include(o => o.Vendor)
                .FirstOrDefault();
            return user;
        }

        public async Task<User> GetUserDetailsByCustomId(string userCustomId)
        {
            var user = _dbContext.AspNetUsers.AsQueryable().Where(w => w.IsActive == true && w.UserCustomId == userCustomId)
                .Include(o => o.Country)
                .ThenInclude(o => o.Region)
                .Include(o => o.Client)
                .Include(o => o.Vendor)
                .FirstOrDefault();
            return user;
        }

        public async Task<User> GetUserDetailsBySID(int sID)
        {
            var user = _dbContext.AspNetUsers.AsQueryable().Where(w => w.IsActive == true && w.SId == sID)
                .Include(o => o.Country)
                .ThenInclude(o => o.Region)
                .Include(o => o.Client)
                .Include(o => o.Vendor)
                .FirstOrDefault();
            return user;
        }

        public async Task<List<User>> GetUserDetailsByClientId(int clientId)
        {
            var user = _dbContext.AspNetUsers.AsQueryable().Where(w => w.IsActive == true && w.ClientId == clientId)
                .Include(o => o.Country)
                .ThenInclude(o => o.Region)
                .Include(o => o.Client)
                .Include(o => o.Vendor)
                .ToList();
            return user;
        }

        public async Task UpdateUserDetails(User user)
        {
            var userDetails = _dbContext.AspNetUsers.AsQueryable().Where(w => w.SId == user.SId && w.UserCustomId == user.UserCustomId).FirstOrDefault();
            if(userDetails != null)
            {
                userDetails.FirstName = user.FirstName;
                userDetails.LastName = user.LastName;
                userDetails.ClientId = user.ClientId == 0 ? null : user.ClientId;
                userDetails.VendorId = user.VendorId == 0 ? null : user.VendorId;
                userDetails.DepartmentId = user.DepartmentId;
                userDetails.Telephone = user.Telephone;
                userDetails.Cellphone = user.Cellphone;
                userDetails.Observation = user.Observation;

                _dbContext.Entry(userDetails).State = EntityState.Modified;
            }
        }

        public async Task DeleteUser(string emailId)
        {
            var userDetails = _dbContext.AspNetUsers.AsQueryable().Where(w=> w.Email == emailId).FirstOrDefault();
            if (userDetails != null)
            {
                userDetails.IsActive = false;
            }
        } 
    }
}
