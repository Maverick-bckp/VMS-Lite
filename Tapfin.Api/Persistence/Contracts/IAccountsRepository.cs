using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IAccountsRepository
    {
        Task UpdateUserCustomId(int sid, string customId);
        User GetUserDetails(string emailId);
        Task<User> GetUserDetailsByCustomId(string userCustomId);
        Task<User> GetUserDetailsBySID(int sID);
        Task<List<User>> GetUserDetailsByClientId(int clientId);
        Task UpdateUserDetails(User user);
        Task DeleteUser(string emailId);
    }
}
