using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class AccountsServices : IAccountsServices
    {
        Result _result = new Result();
        IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly ITokenServices _tokenServices;
        private readonly IUnitOfWork _uow;
        User currentUser;
        private readonly string defaultPassword = "Admin@12345";

        public AccountsServices(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper, ITokenServices tokenServices, IUnitOfWork uow)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _tokenServices = tokenServices;
            _uow = uow;

            currentUser = GetLoggedInUser();
        }
        public async Task<dynamic> RegisterUser(RegisterUserDtoRequest request)
        {
            var userMapped = _mapper.Map<User>(request);
            userMapped.CreatedAt = DateTime.UtcNow;
            userMapped.CreatedBy = currentUser.UserCustomId;
            userMapped.IsActive = true;


            /*------------- Get CountryId by ClientId--------------*/
            if (request.ClientId != null)
            {
                var clientId = Convert.ToInt32(request.ClientId);
                var clientData = await _uow.clientRepository.getClientDetailsById(clientId);
                userMapped.CountryId = clientData.CountryId;
            }

            /*------------- Get CountryId by VendorId --------------*/
            if (request.VendorId != null)
            {
                var vendorId = Convert.ToInt32(request.VendorId);
                var vendorData = await _uow.vendorRepository.getVendorDetailsById(vendorId);
                userMapped.CountryId = vendorData.CountryId;
            }


            /*--------- Validate Email ID if exists----------*/
            var users = await _userManager.FindByEmailAsync(request.Email);
            if (users != null)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "User emailId is already registered. Please try again with another emailId.");
            }

            /*----------- Create/Register User ------------*/
            var result = await _userManager.CreateAsync(userMapped, defaultPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(s => s.Description);

                return _result.AddReturnData(HttpStatusCode.BadRequest, errors, "User is not registered. Please check user details and try again.");
            }

            /*-------------- Update UserCustomID--------------*/
            var customID = CreateUserCustomID(userMapped.SId);
            await _uow.accountsRepository.UpdateUserCustomId(userMapped.SId, customID);
            int status = await _uow.SaveChangesAsync();


            /*--------------- Add User Role --------------*/
            var createdUser = await _userManager.FindByIdAsync(userMapped.Id);
            var roleStatus = await _userManager.AddToRolesAsync(createdUser, request.Roles);



            return _result.AddReturnData(HttpStatusCode.Created, null, "User is registered successfully.");
        }

        public async Task<dynamic> UpdateUser(UpdateUserDetailsDtoRequest request)
        {
            var userDetails = _mapper.Map<User>(request);

            await _uow.accountsRepository.UpdateUserDetails(userDetails);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "User data cannnot be updated. Please try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "User details is updated successfully.");
        }

        public async Task<dynamic> AuthenticateUser(UserAuthRequestDto request)
        {
            dynamic obj = new JObject();


            var user = await _userManager.FindByNameAsync(request.Username!);
            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password!))
            {
                return _result.AddReturnData(HttpStatusCode.Unauthorized, message: "Invalid Authentication.");
            }

            if (user.IsActive == false)
            {
                return _result.AddReturnData(HttpStatusCode.Unauthorized, message: "Your account is not active. Please contact admin.");
            }

            var roles = await _userManager.GetRolesAsync(user);


            var token = _tokenServices.CreateToken(user, roles);
            obj.Roles = JArray.Parse(JsonConvert.SerializeObject(roles.ToArray()));
            obj.Token = token;

            return _result.AddReturnData(HttpStatusCode.OK, obj, message: "User is authenticated.");
        }

        public async Task<dynamic> CreateRole(CreateRoleDtoRequest request)
        {
            Role role = new Role
            {
                Name = request.Name,
                NormalizedName = request.NormalizedName,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Description = request.RoleDescription,
            };

            IdentityResult result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "User Role is not created.");
            }


            return _result.AddReturnData(HttpStatusCode.Created, message: "User Role is created.");
        }


        public async Task<dynamic> DeleteUser(DeleteUserRequestDto request)
        {
            var users = await _userManager.FindByEmailAsync(request.EmailId);
            if (users == null)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, null, "User is not found. Please try again");
            }

            await _uow.accountsRepository.DeleteUser(request.EmailId);
            var deleteStatus = await _uow.SaveChangesAsync();
            if (deleteStatus < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, null, "User is not deleted. Please try again");
            }

            return _result.AddReturnData(HttpStatusCode.OK, null, message: "User is deleted successfully");
        }

        public async Task<dynamic> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return _result.AddReturnData(HttpStatusCode.OK, roles, message: "All user roles has been fetched.");
        }

        public async Task<dynamic> UserProfile()
        {
            var user = GetLoggedInUser();
            var userMapped = _mapper.Map<UserProfileResponseDto>(user);
            if (user.Country != null)
            {
                userMapped.Region = _mapper.Map<UserProfileRegionDto>(user.Country.Region);
            }

            /*-------- Get User Role -------*/
            var roles = await _userManager.GetRolesAsync(user);

            /*------- Create JSON Response---------*/
            dynamic userJsonResponse = new JObject();
            userJsonResponse.Profile = JObject.Parse(JsonConvert.SerializeObject(userMapped));
            userJsonResponse.Roles = JArray.Parse(JsonConvert.SerializeObject(roles.ToArray()));

            return _result.AddReturnData(HttpStatusCode.OK, userJsonResponse, "User profile data has been fetched successfully.");
        }

        public async Task<dynamic> UserProfileBySID(int sID)
        {
            var user = await _uow.accountsRepository.GetUserDetailsBySID(sID);
            var userMapped = _mapper.Map<UserProfileResponseDto>(user);
            userMapped.Region = _mapper.Map<UserProfileRegionDto>(user.Country.Region);

            /*-------- Get User Role -------*/
            var roles = await _userManager.GetRolesAsync(user);

            /*------- Create JSON Response---------*/
            dynamic userJsonResponse = new JObject();
            userJsonResponse.Profile = JObject.Parse(JsonConvert.SerializeObject(userMapped));
            userJsonResponse.Roles = JArray.Parse(JsonConvert.SerializeObject(roles.ToArray()));

            return _result.AddReturnData(HttpStatusCode.OK, userJsonResponse, "User profile data has been fetched successfully.");
        }

        public User GetLoggedInUser()
        {
            User loggedInUser = new User();

            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                var email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                var role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

                loggedInUser = _uow.accountsRepository.GetUserDetails(email);
            }

            return loggedInUser;
        }

        public string GetLoggedInUserRole()
        {
            string role = string.Empty;
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                var email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            }

            return role;
        }

        public User GetUserDetailsByEmailId(string emailId)
        {
            var userDetails = _uow.accountsRepository.GetUserDetails(emailId);
            return userDetails;
        }

        public async Task<User> GetUserDetailsByCustomId(string userCustomId)
        {
            var userDetails = await _uow.accountsRepository.GetUserDetailsByCustomId(userCustomId);
            return userDetails;
        }

        public async Task<List<User>> GetUserDetailsByClientId(int clientId)
        {
            var userDetails = await _uow.accountsRepository.GetUserDetailsByClientId(clientId);
            return userDetails;
        }

        private string CreateUserCustomID(int sid)
        {
            string userCustomId = string.Empty;

            if (sid <= 100)
            {
                userCustomId = $"CU{sid.ToString().PadLeft(3, '0')}";
            }

            if (sid > 100)
            {
                userCustomId = $"CU{sid}";
            }

            return userCustomId;
        }

        public async Task<dynamic> ForgotPassword(ForgotPasswordRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "User could not be found. Please check and try again.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, message: "Password reset token has been generated. Please check your email.");
        }

        public async Task<dynamic> EmailTest(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("smartrac@manpoweronline.in"));
            email.To.Add(MailboxAddress.Parse("abhijit.sen@in.experis.com"));
            email.Subject = "Test Subject";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("4.213.47.32", 587,MailKit.Security.SecureSocketOptions.Auto);
            smtp.Authenticate("smartrac@manpoweronline.in", "TNdH-3YFKR");
            smtp.Send(email);
            smtp.Disconnect(true);

            return _result.AddReturnData(HttpStatusCode.OK, null, message: "Mail has been sent successfully");
        }
    }
}
