using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class WorkerDetailsDto
    {
    }

    public class GetWorkerDetailsDtoResponse
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int? JobOrderId { get; set; }
        public string? HiringManagerId { get; set; }
        public string? AccountManagerId { get; set; }
        public string? ServiceTypeId { get; set; }
        public string? WorkerName { get; set; }
        public string? WorkerCode { get; set; }
        public string? CPF { get; set; }
        public float? Salary { get; set; }
        public string? RecruiterName { get; set; }
        public string? IdentificationNo { get; set; }
        public DateTime? HiringDate { get; set; }
        public string? WorkerLocation { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ReasonForHiring { get; set; }
        public int? DepartmentId { get; set; }
        public string? PersonalPhone { get; set; }
        public string? WorkPhone { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string? ReasonForTermination { get; set; }
        public string? PersonalEmail { get; set; }
        public string? WorkEmail { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractExpiryDate { get; set; }
        public string? MonthlyWorkload { get; set; }
        public int? ContractType { get; set; }
        public int? AllocatedAtClientId { get; set; }
        public string? ZipCode { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Neighbourhood { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public int? WorkerStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

        [JsonProperty("Client")]
        public GetWorkerClientDetails ClientDetail { get; set; }

        [JsonProperty("Vendor")]
        public GetWorkerVendorDetails VendorDetail { get; set; }

        [JsonProperty("ContractStatusType")]
        public GetWorkerDetailsContractStatusType WorkerContractStatusType { get; set; }

        [JsonProperty("WorkerStatusType")]
        public GetWorkerDetailsStatusType WorkerStatusType { get; set; }

        //[JsonProperty("EquipmentCost")]
        //public List<GetWorkerDetailsEquipmentCostDto> WorkerEquipmentCost { get; set; }
    }

    public class GetWorkerFullDetailsDtoResponse
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int? JobOrderId { get; set; }
        public string? HiringManagerId { get; set; }
        public string? AccountManagerId { get; set; }
        public string? ServiceTypeId { get; set; }
        public string? WorkerName { get; set; }
        public string? WorkerCode { get; set; }
        public string? CPF { get; set; }
        public float? Salary { get; set; }
        public string? RecruiterName { get; set; }
        public string? IdentificationNo { get; set; }
        public DateTime? HiringDate { get; set; }
        public string? WorkerLocation { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ReasonForHiring { get; set; }
        public int? DepartmentId { get; set; }
        public string? PersonalPhone { get; set; }
        public string? WorkPhone { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string? ReasonForTermination { get; set; }
        public string? PersonalEmail { get; set; }
        public string? WorkEmail { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractExpiryDate { get; set; }
        public string? MonthlyWorkload { get; set; }
        public int? ContractType { get; set; }
        public int? AllocatedAtClientId { get; set; }
        public string? ZipCode { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Neighbourhood { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public int? WorkerStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

        [JsonProperty("Client")]
        public GetWorkerClientDetails ClientDetail { get; set; }

        [JsonProperty("Vendor")]
        public GetWorkerVendorDetails VendorDetail { get; set; }

        [JsonProperty("ContractStatusType")]
        public GetWorkerDetailsContractStatusType WorkerContractStatusType { get; set; }

        [JsonProperty("WorkerStatusType")]
        public GetWorkerDetailsStatusType WorkerStatusType { get; set; }

        [JsonProperty("EquipmentCost")]
        public List<GetWorkerDetailsEquipmentCostDto> WorkerEquipmentCost { get; set; }

        [JsonProperty("JobDetail")]
        public GetWorkerFullDetailsJobDetailsDto? JobDetail { get; set; }

        [JsonProperty("JobOrder")]
        public GetWorkerFullDetailsJobOrderDetailsDto? JobOrder { get; set; }

        [JsonProperty("Department")]
        public GetWorkerFullDetailsDepartmentDto? Department { get; set; }

        [JsonProperty("AllocatedAtClient")]
        public GetWorkerFullDetailsAllocatedAtClientDto? AllocatedAtClient { get; set; }

        [JsonProperty("HiringManager")]
        public GetWorkerFullDetailsHiringManagerDetailsDto? HiringManagerDetails { get; set; }

        [JsonProperty("AccountManager")]
        public GetWorkerFullDetailsAccountManagerDetailsDto? AccountManagerDetails { get; set; }
    }

    public class GetWorkerDetailsStatusType
    {
        public int Id { get; set; }
        public string StatusTypeDesc { get; set; } = null!;
    }

    public class GetWorkerDetailsContractStatusType
    {
        public int Id { get; set; }
        public string StatusTypeDesc { get; set; } = null!;
    }

    public class GetWorkerClientDetails
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string ClientCode { get; set; } = null!;
        public string TIN { get; set; }
        public string BusinessName { get; set; }
        public string TradeName { get; set; }
        public DateTime DateOfEstablishment { get; set; }
        public string StateRegistration { get; set; }
        public string MunicipalRegistration { get; set; }
        public string Site { get; set; }
        public DateTime ClientSince { get; set; }
        public DateTime ClosureDate { get; set; }
        public string Observations { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class GetWorkerVendorDetails
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string VendorCode { get; set; } = null!;
        public DateTime? StartOfServiceProvision { get; set; }
        public DateTime? EndOfServiceProvision { get; set; }
        public string? ContractNo { get; set; }
        public string? ServiceDescription { get; set; }
        public string? NationalRegistryOfLegalEntities { get; set; }
        public string? BusinessName { get; set; }
        public string? TradeName { get; set; }
        public DateTime? DateOfEstablishment { get; set; }
        public string? StateRegistration { get; set; }
        public string? MunicipalRegistration { get; set; }
        public string? Site { get; set; }
        public DateTime? ClientSince { get; set; }
        public DateTime? ClosureDate { get; set; }
        public string? Observations { get; set; }
        public string? DefaultVendor { get; set; }
    }

    public class GetWorkerDetailsEquipmentCostDto
    {
        public int Id { get; set; }
        public string? Reference { get; set; }
        public double Amount { get; set; }
        public string? Remarks { get; set; }
        public string? Burden { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class GetWorkerFullDetailsJobDetailsDto
    {
        public int Id { get; set; }
        public int? ServiceTypeId { get; set; }
        public string JobCode { get; set; } = null!;
        public string? JobTitle { get; set; }
        public string? JobDesc { get; set; }
        public string JobLocation { get; set; } = null!;
        public int NoOfPosition { get; set; }
        public int? YearsOfExperience { get; set; }
        public double? ExpectedSalary { get; set; }
        public string? Remarks { get; set; }
        public int JobStatus { get; set; }
    }

    public class GetWorkerFullDetailsJobOrderDetailsDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string JobOrderCode { get; set; } = null!;
        public int VendorId { get; set; }
        public int JobOrderStatus { get; set; }
    }

    public class GetWorkerFullDetailsDepartmentDto
    {
        public int Id { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
    }

    public class GetWorkerFullDetailsAllocatedAtClientDto
    {
        public int Id { get; set; }
        public string TypeDesc { get; set; } = null!;
    }

    public class GetWorkerFullDetailsHiringManagerDetailsDto
    {
        public int SId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserCustomId { get; set; }
        public string? Email { get; set; }
    }

    public class GetWorkerFullDetailsAccountManagerDetailsDto
    {
        public int SId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserCustomId { get; set; }
        public string? Email { get; set; }
    }

    public class CreateWorkerDetailsDtoRequest
    {
        public int? JobId { get; set; }
        public int? JobOrderId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string? HiringManagerId { get; set; }
        [Required]
        public int? ServiceTypeId { get; set; }
        [Required]
        public string? WorkerName { get; set; }
        [Required]
        public string? WorkerCode { get; set; }
        [Required]
        public string? CPF { get; set; }
        [Required]
        public float? Salary { get; set; }
        [Required]
        public string? RecruiterName { get; set; }
        [Required]
        public string? IdentificationNo { get; set; }
        [Required]
        public DateTime? HiringDate { get; set; }
        [Required]
        public string? WorkerLocation { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string? ReasonForHiring { get; set; }
        [Required]
        public int? DepartmentId { get; set; }
        [Required]
        public string? PersonalPhone { get; set; }
        [Required]
        public string? WorkPhone { get; set; }
        [Required]
        public DateTime? TerminationDate { get; set; }
        [Required]
        public string? ReasonForTermination { get; set; }
        public string? PersonalEmail { get; set; }
        [Required]
        public string? WorkEmail { get; set; }
        [Required]
        public DateTime? ContractStartDate { get; set; }
        [Required]
        public DateTime? ContractExpiryDate { get; set; }
        [Required]
        public string? MonthlyWorkload { get; set; }
        [Required]
        public int? ContractType { get; set; }
        [Required]
        public int? AllocatedAtClientId { get; set; }
        public string? ZipCode { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Neighbourhood { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public int? WorkerStatus { get; set; }
    }

    public class UpdateWorkerDetailsDtoRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int JobId { get; set; }
        public int? JobOrderId { get; set; }
        [Required]
        public int? ServiceTypeId { get; set; }
        [Required]
        public string? WorkerName { get; set; }
        [Required]
        public string? WorkerCode { get; set; }
        [Required]
        public string? CPF { get; set; }
        [Required]
        public float? Salary { get; set; }
        [Required]
        public string? RecruiterName { get; set; }
        [Required]
        public string? IdentificationNo { get; set; }
        [Required]
        public DateTime? HiringDate { get; set; }
        [Required]
        public string? WorkerLocation { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string? ReasonForHiring { get; set; }
        [Required]
        public int? DepartmentId { get; set; }
        [Required]
        public string? PersonalPhone { get; set; }
        [Required]
        public string? WorkPhone { get; set; }
        [Required]
        public DateTime? TerminationDate { get; set; }
        [Required]
        public string? ReasonForTermination { get; set; }
        public string? PersonalEmail { get; set; }
        [Required]
        public string? WorkEmail { get; set; }
        [Required]
        public DateTime? ContractStartDate { get; set; }
        [Required]
        public DateTime? ContractExpiryDate { get; set; }
        public string? MonthlyWorkload { get; set; }
        [Required]
        public int? ContractType { get; set; }
        [Required]
        public int? AllocatedAtClientId { get; set; }
        public string? ZipCode { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Neighbourhood { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public int? WorkerStatus { get; set; }
    }
}
