using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class WorkerPayrollDto
    {

    }

    public class GetPayrollWorkerListRequest
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string PayrollMonth { get; set; }
        [Required]
        public string PayrollYear { get; set; }
    }

    public class UploadPayrollWorkerDetailsRequest
    {
        [Required]
        public IFormFile File { get; set; }
    }

    public class UploadPayrollWorkerExcelData
    {
        public int WorkerId { get; set; }
        public string WorkerCode { get; set; } = null!;
        public string WorkerName { get; set; } = null!;
        public string PayrollMonth { get; set; } = null!;
        public string PayrollYear { get; set; } = null!;
        public double Amount { get; set; }
        public string? Comment { get; set; }
    }

    public class UploadPayrollWorkerExcelErrorLog
    {
        public string? WorkerId { get; set; }
        public string? WorkerCode { get; set; } 
        public string? WorkerName { get; set; } 
        public string? PayrollMonth { get; set; } 
        public string? PayrollYear { get; set; }
        public double? Amount { get; set; } = null;
        public string? Comment { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
