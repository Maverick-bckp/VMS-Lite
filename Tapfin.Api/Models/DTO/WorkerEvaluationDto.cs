using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class WorkerEvaluationDto
    {

    }

    public class GetWorkerEvaluationDetailsDtoResponse
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public string? Evaluator { get; set; }
        public string? Message { get; set; }
        public DateTime? Date { get; set; }
        public int? Rating { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CreateWorkerEvaluationDetailsDtoRequest
    {
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public string Evaluator { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Rating { get; set; }
    }

    public class UpdateWorkerEvaluationDetailsDtoRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Evaluator { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}
