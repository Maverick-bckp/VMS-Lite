namespace Tapfin.Api.Models.Entities
{
    public class WorkerContractStatusTypes
    {
        public int Id { get; set; }

        public string StatusTypeDesc { get; set; } = null!;

        public bool? IsActive { get; set; }

        public virtual ICollection<WorkerDetail> JobOrder { get; set; } = new List<WorkerDetail>();
    }
}
