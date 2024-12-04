using System.ComponentModel.DataAnnotations.Schema;

namespace Tapfin.Api.Models.Entities
{
    public class WorkerExtension
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }

        public string? Message { get; set; }

        public DateTime? ExtensionDate { get; set; }

        public int Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public bool? IsActive { get; set; }

        [ForeignKey("WorkerId")]
        public WorkerDetail WorkerDetail { get; set; }

        [ForeignKey("Status")]
        public WorkerExtensionStatusTypes WorkerExtensionStatusTypes { get; set; }
    }
}
