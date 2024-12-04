using System.ComponentModel.DataAnnotations.Schema;

namespace Tapfin.Api.Models.Entities
{
    public class WorkerBackgroundEvaluation
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }

        public string? Type { get; set; }

        public string? Contact { get; set; }

        public string? Institution { get; set; }

        public string? Telephone { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Description { get; set; }

        public int Validation { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public bool? IsActive { get; set; }

        [ForeignKey("Validation")]
        public WorkerBckgEvalValidationTypes ValidationTypes { get; set; }
    }
}
