using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class WorkerExtensionDto
    {
        
    }

    public class GetWorkerExtensionDetailsDtoResponse
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public string? Message { get; set; }
        public DateTime? ExtensionDate { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }

        [JsonProperty("StatusType")]
        public GeWorkerExtensionDetailsStatusTypesDtoResponse WorkerExtensionStatusTypes { get; set; }
    }

    public class GeWorkerExtensionDetailsStatusTypesDtoResponse
    {
        public int Id { get; set; }
        public string StatusTypeDesc { get; set; }
    }

    public class CreateWorkerExtensionDetailsDtoRequest
    {
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public string? Message { get; set; }
        [Required]
        public DateTime? ExtensionDate { get; set; }
        [Required]
        public int? Status { get; set; }
    }

    public class UpdateWorkerExtensionDetailsDtoRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public string? Message { get; set; }
        [Required]
        public DateTime? ExtensionDate { get; set; }
        [Required]
        public int? Status { get; set; }
    }
}
