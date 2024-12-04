using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class RegionDto
    {

    }

    public class GetRegionDtoResponse
    {
        public int Id { get; set; }
        public string RegionCode { get; set; } = null!;
        public string RegionName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CreateRegionDtoRequest
    {
        [Required]
        public string RegionCode { get; set; }
        [Required]
        public string RegionName { get; set; }
    }

    public class UpdateRegionDtoRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string RegionCode { get; set; }
        [Required]
        public string RegionName { get; set; }
    }
}
