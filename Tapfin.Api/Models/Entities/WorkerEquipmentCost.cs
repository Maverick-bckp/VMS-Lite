using System.ComponentModel.DataAnnotations.Schema;

namespace Tapfin.Api.Models.Entities
{
    public class WorkerEquipmentCost
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }

        public string? Reference { get; set; }

        public double Amount { get; set; }

        public string? Remarks { get; set; }

        public string? Burden { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public bool? IsActive { get; set; }

        [ForeignKey("WorkerId")]
        public WorkerDetail WorkerDetail { get; set; }
    }
}
