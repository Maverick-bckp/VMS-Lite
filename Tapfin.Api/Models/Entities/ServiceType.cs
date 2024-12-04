namespace Tapfin.Api.Models.Entities
{
    public class ServiceType
    {
        public int Id { get; set; }

        public string ServiceTypeDesc { get; set; } = null!;

        public bool? IsActive { get; set; }

        public virtual ICollection<JobDetail> JobDetails { get; set; } = new List<JobDetail>();
        public virtual ICollection<WorkerDetail> WorkerDetail { get; set; } = new List<WorkerDetail>();
    }
}
