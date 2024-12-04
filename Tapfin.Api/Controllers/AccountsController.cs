using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsServices _accountsServices;

        public AccountsController(IAccountsServices accountsServices)
        {
            _accountsServices = accountsServices;
        }

        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = "SuperAdmin,ClientAdminOps,VendorAdminOps")]
        public async Task<dynamic> RegisterUser([FromBody] RegisterUserDtoRequest request)
        {
            var result = await _accountsServices.RegisterUser(request);
            return result;
        }

        [HttpPost]
        [Route("Profile/Update")]
        [Authorize(Roles = "SuperAdmin,ClientAdminOps,VendorAdminOps")]
        public async Task<dynamic> UpdateUser([FromBody] UpdateUserDetailsDtoRequest request)
        {
            var result = await _accountsServices.UpdateUser(request);
            return result;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<dynamic> LoginUser([FromBody] UserAuthRequestDto request)
        {
            var result = await _accountsServices.AuthenticateUser(request);
            return result;
        }

        [HttpGet]
        [Route("Profile")]
        [Authorize]
        public async Task<dynamic> UserProfile()
        {
            var result = await _accountsServices.UserProfile();
            return result;
        }


        [HttpGet]
        [Route("Profile/{SId}")]
        [Authorize]
        public async Task<dynamic> GetUserDetailsBySID(int SId)
        {
            var result = await _accountsServices.UserProfileBySID(SId);
            return result;
        }


        [HttpPost]
        [Route("Role/Create")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<dynamic> CreateRole([FromBody] CreateRoleDtoRequest request)
        {
            var result = await _accountsServices.CreateRole(request);
            return result;
        }

        [HttpGet]
        [Route("Role/List")]
        [Authorize(Roles = "SuperAdmin,ClientAdminOps,VendorAdminOps")]
        public async Task<dynamic> GetRoles()
        {
            var result = await _accountsServices.GetRoles();
            return result;
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<dynamic> ForgotPassword(ForgotPasswordRequestDto request)
        {
            var result = await _accountsServices.ForgotPassword(request);
            return result;
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "SuperAdmin,ClientAdminOps,VendorAdminOps")]
        public async Task<dynamic> DeleteUser([FromBody] DeleteUserRequestDto request)
        {
            var result = await _accountsServices.DeleteUser(request);
            return result;
        }

        [HttpPost]
        [Route("Mail")]
        //[Authorize()]
        public async Task<dynamic> MailUser([FromQuery]string mailBody)
        {
            var result = await _accountsServices.EmailTest(mailBody);
            return result;
        }
    }
}
