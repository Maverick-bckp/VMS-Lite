namespace Tapfin.Api.Models.Entities
{
    public class JobOrderStatusType
    {
        public int Id { get; set; }

        public string StatusTypeDesc { get; set; } = null!;

        public bool? IsActive { get; set; }

        public virtual ICollection<JobOrder> JobOrder { get; set; } = new List<JobOrder>();
    }
}
