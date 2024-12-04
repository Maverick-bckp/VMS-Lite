using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class JobOrdersDto
    {

    }

    public class GetJobOrdersDtoResponse
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string VendorId { get; set; }
        public string JobOrderCode { get; set; } = null!;
        public int JobOrderStatus { get; set; } 
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsWorkerAvailable { get; set; }
        public JobOrdersVendorDto Vendor { get; set; }
        public JobOrdersStatusTypeDto JobOrderStatusType { get; set; }
        public UserBasicdetailsDto User { get; set; }
        public List<JobOrdersWorkerDetailsDto>? WorkerDetail { get; set; }
    }

    public class JobOrdersVendorDto
    {
        public int Id { get; set; }
        public string VendorCode { get; set; }
        public string? BusinessName { get; set; }
        public string? TradeName { get; set; }
    }

    public class JobOrdersStatusTypeDto
    {
        public int Id { get; set; }
        public string StatusTypeDesc { get; set; }
    }

    public class JobOrdersWorkerDetailsDto
    {
        public int Id { get; set; }
        public string? WorkerName { get; set; }
        public string? WorkerCode { get; set; }
        public string? IdentificationNo { get; set; }
        public string? WorkEmail { get; set; }
    }

    public class GetJobOrdersStatusTypesDtoResponse
    {
        public int Id { get; set; }
        public string StatusTypeDesc { get; set; }
    }

    public class CreateJobOrderDtoRequest
    {
        [Required]
        public int JobId { get; set; }
        [Required]
        public string JobOrderCode { get; set; }
        [Required]
        public string VendorId { get; set; }
        [Required]
        public string JobOrderStatus { get; set; }
    }

    public class UpdateJobOrderDtoRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int JobId { get; set; }
        [Required]
        public string JobOrderCode { get; set; }
        [Required]
        public string VendorId { get; set; }
        [Required]
        public string JobOrderStatus { get; set; }
    }
}
