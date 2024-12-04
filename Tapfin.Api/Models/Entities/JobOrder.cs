using System.ComponentModel.DataAnnotations.Schema;

namespace Tapfin.Api.Models.Entities
{
    public class JobOrder
    {
        public int Id { get; set; }

        public int JobId { get; set; }

        public string JobOrderCode { get; set; } = null!;

        public int VendorId { get; set; }

        public int JobOrderStatus { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public bool? IsActive { get; set; }

        public virtual VendorDetail Vendor { get; set; } = null!;

        public virtual JobOrderStatusType JobOrderStatusType { get; set; } = null!;

        public virtual ICollection<WorkerDetail>? WorkerDetail { get; set; }
    }
}
