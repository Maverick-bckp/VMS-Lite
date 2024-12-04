using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class RegisterDto
    {
    }

    public class RegisterUserDtoRequest
    {
        [Required]
        public string Firstname { get; set; } = null!;
        [Required]
        public string? Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        public int? ClientId { get; set; }
        public int? VendorId { get; set; }
        public int? DepartmentId { get; set; }
        [Required]
        public string? Telephone { get; set; }
        public string? Cellphone { get; set; }
        public string? Observation { get; set; }
        [Required]
        public string[] Roles { get; set; }
    }

    public class UpdateUserDetailsDtoRequest
    {
        [Required]
        public int SId { get; set; }
        [Required]
        public string? UserCustomId { get; set; }
        [Required]
        public string Firstname { get; set; } = null!;
        public string? Lastname { get; set; }
        public int? ClientId { get; set; } = null;
        public int? VendorId { get; set; } = null;
        public int? DepartmentId { get; set; }
        public string? Telephone { get; set; }
        public string? Cellphone { get; set; }
        public string? Observation { get; set; }
    }
    public class RegisterUserDtoResponse
    {
        
    }
}
