using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class WorkerEquipmentCostDto
    {

    }

    public class GetWorkerEquipmentCostDetailsDtoResponse
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public string? Reference { get; set; }
        public double Amount { get; set; }
        public string? Remarks { get; set; }
        public string? Burden { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CreateWorkerEquipmentCostDetailsDtoRequest
    {
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public string Burden { get; set; }
    }

    public class UpdateWorkerEquipmentCostDetailsDtoRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public string Burden { get; set; }
    }
}
