namespace Tapfin.Api.Models.Entities
{
    public class WorkerPayroll
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }

        public string? PayrollMonth { get; set; }

        public string? PayrollYear { get; set; }

        public double? Amount { get; set; }

        public string? Comment { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public bool? IsActive { get; set; }
    }
}
