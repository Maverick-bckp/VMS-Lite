using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class WorkerBackgroundEvaluationDto
    {
    }

    public class GetWorkerBckgEvaluationDetailsDtoResponse
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
        public bool? IsActive { get; set; }
    }

    public class GetWorkerBckgEvaluationValidationTypesDtoResponse
    {
        public int Id { get; set; }
        public string StatusTypeDesc { get; set; }
    }

    public class CreateWorkerBckgEvaluationDetailsDtoRequest
    {
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public string? Contact { get; set; }
        [Required]
        public string? Institution { get; set; }
        [Required]
        public string? Telephone { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Validation { get; set; }
    }

    public class UpdateWorkerBckgEvaluationDetailsDtoRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public string? Contact { get; set; }
        [Required]
        public string? Institution { get; set; }
        [Required]
        public string? Telephone { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Validation { get; set; }
    }
}
