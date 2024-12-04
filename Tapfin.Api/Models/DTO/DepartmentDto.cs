using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class DepartmentDto
    {

    }

    public class GetDepartmentDtoResponse
    {
        public int Id { get; set; }
        public string DepartmentCode { get; set; } 
        public string DepartmentName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CreateDepartmentDtoRequest
    {
        [Required]
        public string DepartmentCode { get; set; }
        [Required]
        public string DepartmentName { get; set; }
    }

    public class UpdateDepartmentDtoRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string DepartmentCode { get; set; }
        [Required]
        public string DepartmentName { get; set; }
    }
}
