namespace Tapfin.Api.Models.Entities
{
    public class WorkerEvaluation
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }

        public string? Evaluator { get; set; }

        public string? Message { get; set; }

        public DateTime? Date { get; set; }

        public int? Rating { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public bool? IsActive { get; set; }
    }
}
