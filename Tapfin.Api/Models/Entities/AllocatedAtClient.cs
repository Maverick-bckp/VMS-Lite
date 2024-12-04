namespace Tapfin.Api.Models.Entities
{
    public class AllocatedAtClient
    {
        public int Id { get; set; }

        public string TypeDesc { get; set; } = null!;

        public bool? IsActive { get; set; }

        public virtual ICollection<JobDetail> JobDetails { get; set; } = new List<JobDetail>();
        public virtual ICollection<WorkerDetail> WorkerDetail { get; set; } = new List<WorkerDetail>();
    }
}
