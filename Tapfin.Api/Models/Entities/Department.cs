namespace Tapfin.Api.Models.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<JobDetail> JobDetails { get; set; } = new List<JobDetail>();
        public virtual ICollection<WorkerDetail> WorkerDetail { get; set; } = new List<WorkerDetail>();
    }
}
