using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class AccountsDto
    {

    }

    public class UserAuthRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class CreateRoleDtoRequest
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string RoleDescription { get; set; }
    }

    public class UserProfileResponseDto
    {
        public int SId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserCustomId { get; set; }
        public string? Email { get; set; }
        public int CountryId { get; set; }
        public int? ClientId { get; set; }
        public int? VendorId { get; set; }
        public int? DepartmentId { get; set; }
        public string? Telephone { get; set; }
        public string? Cellphone { get; set; }
        public string? Observation { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

        public UserProfileRegionDto Region { get; set; }
        public UserProfileCountryDto Country { get; set; }
        public UserProfileClientDto Client { get; set; }
        public UserProfileVendorDto Vendor { get; set; }
    }

    public class UserProfileRegionDto
    {
        public int Id { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
    }

    public class UserProfileCountryDto
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
    }

    public class UserProfileClientDto
    {
        public int Id { get; set; }
        public string ClientCode { get; set; }
    }

    public class UserProfileVendorDto
    {
        public int Id { get; set; }
        public string VendorCode { get; set; }
    }

    public class ForgotPasswordRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class DeleteUserRequestDto
    {
        public string EmailId { get; set; }
    }
}
