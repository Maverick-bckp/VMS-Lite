using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tapfin.Api.Models.Entities
{
    public class WorkerDetail
    {
        [Key]
        public int Id { get; set; }

        public int? JobId { get; set; }

        public int? JobOrderId { get; set; }

        public int? CountryId { get; set; }

        public int ClientId { get; set; }

        public int? VendorId { get; set; }

        public string? HiringManagerId { get; set; }

        public string? AccountManagerId { get; set; }

        public int? ServiceTypeId { get; set; }

        public string? WorkerName { get; set; }

        public string? WorkerCode { get; set; }

        public string? CPF { get; set; }

        public double? Salary { get; set; }

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

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public bool? IsActive { get; set; }

        [ForeignKey("JobId")]
        public virtual JobDetail JobDetail { get; set; }

        [ForeignKey("JobOrderId")]
        public virtual JobOrder JobOrder { get; set; }

        [ForeignKey("AllocatedAtClientId")]
        public virtual AllocatedAtClient AllocatedAtClient { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        [ForeignKey("ClientId")]
        public virtual ClientDetail ClientDetail { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorDetail VendorDetail { get; set; }

        [ForeignKey("ServiceTypeId")]
        public virtual ServiceType ServiceType { get; set; }


        [ForeignKey("WorkerStatus")]
        public virtual WorkerStatusTypes WorkerStatusType { get; set; }

        [ForeignKey("ContractType")]
        public virtual WorkerContractStatusTypes WorkerContractStatusType { get; set; }


        public virtual ICollection<WorkerEquipmentCost> WorkerEquipmentCost { get; set; }
    }
}
