using Microsoft.AspNetCore.Mvc;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Services.Contracts
{
    public interface IAccountsServices
    {
        Task<dynamic> RegisterUser(RegisterUserDtoRequest request);
        Task<dynamic> UpdateUser(UpdateUserDetailsDtoRequest request);
        Task<dynamic> AuthenticateUser(UserAuthRequestDto request);
        User GetLoggedInUser();
        string GetLoggedInUserRole();
        Task<dynamic> UserProfile();
        Task<dynamic> UserProfileBySID(int sID);
        Task<dynamic> CreateRole(CreateRoleDtoRequest request);
        Task<dynamic> GetRoles();
        User GetUserDetailsByEmailId(string emailId);
        Task<User> GetUserDetailsByCustomId(string userCustomId);
        Task<dynamic> ForgotPassword(ForgotPasswordRequestDto request);
        Task<dynamic> DeleteUser(DeleteUserRequestDto request);
        Task<dynamic> EmailTest(string body);
    }
}
