using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class CountryDto
    {

    }

    public class GetCountryDtoResponse
    {
        public int Id { get; set; }
        public string RegionId { get; set; }
        public string CountryCode { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public string Currency { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public CountryRegionDto Region { get; set; }
    }

    public class CountryRegionDto
    {
        public int Id { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
    }

    public class CreateCountryDtoRequest
    {
        [Required]
        public int RegionId { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string Currency { get; set; }
    }

    public class UpdateCountryDtoRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int RegionId { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string Currency { get; set; }
    }
}
