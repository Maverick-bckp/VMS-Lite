using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class JobDetailsDto
    {

    }

    public class GetJobDetailsDtoResponse
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CountryId { get; set; }
        public string JobCode { get; set; } = null!;
        public string? JobTitle { get; set; }
        public string? JobDesc { get; set; }
        public string JobLocation { get; set; } = null!;
        public int NoOfPosition { get; set; }
        public int? YearsOfExperience { get; set; }
        public double? ExpectedSalary { get; set; }
        public string? Remarks { get; set; }
        public string JobStatus { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public JobDetailsRegionDto Region { get; set; }
        public JobDetailsCountryDto Country { get; set; }
        public JobDetailsClientDto Client { get; set; }
        public JobDetailsDepartmentDto Department { get; set; }
        public JobDetailsServiceTypeDto ServiceType { get; set; }
        public JobDetailsAllocatedAtClientDto AllocatedAtClient { get; set; }
        public JobDetailsStatusDto JobStatusType { get; set; }
    }

    public class JobDetailsRegionDto
    {
        public int Id { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
    }

    public class JobDetailsCountryDto
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }

    public class JobDetailsClientDto
    {
        public int Id { get; set; }
        public string ClientCode { get; set; }
    }

    public class JobDetailsServiceTypeDto
    {
        public int Id { get; set; }
        public string ServiceTypeDesc { get; set; }
    }

    public class JobDetailsAllocatedAtClientDto
    {
        public int Id { get; set; }
        public string TypeDesc { get; set; }
    }

    public class JobDetailsStatusDto
    {
        public int Id { get; set; }
        public string StatusTypeDesc { get; set; }
    }

    public class JobDetailsDepartmentDto
    {
        public int Id { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
    }

    public class GetJobStatusTypesDtoResponse
    {
        public int Id { get; set; }
        public string StatusTypeDesc { get; set; }
    }

    public class CreateJobDtoRequest
    {       
        [Required]
        public int ServiceTypeId { get; set; } 
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int AllocatedAtClientId { get; set; }
        [Required]
        public string JobCode { get; set; } = null!;
        [Required]
        public string? JobTitle { get; set; }
        [Required]
        public string? JobDesc { get; set; }
        [Required]
        public string? JobLocation { get; set; }
        [Required]
        public int NoOfPosition { get; set; }
        [Required]
        public int? YearsOfExperience { get; set; }
        [Required]
        public double? ExpectedSalary { get; set; }
        public string? Remarks { get; set; }
        [Required]
        public int JobStatus { get; set; }

        public List<IFormFile>? Documents { get; set; } = null;
    }

    public class UpdateJobDetailsDtoRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ServiceTypeId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int AllocatedAtClientId { get; set; }
        [Required]
        public string JobCode { get; set; } = null!;
        [Required]
        public string? JobTitle { get; set; }
        [Required]
        public string? JobDesc { get; set; }
        [Required]
        public string JobLocation { get; set; } = null!;
        [Required]
        public int NoOfPosition { get; set; }
        [Required]
        public int? YearsOfExperience { get; set; }
        [Required]
        public double? ExpectedSalary { get; set; }

        public string? Remarks { get; set; }
        [Required]
        public int JobStatus { get; set; }
    }
}
