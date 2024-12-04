using System.ComponentModel.DataAnnotations.Schema;

namespace Tapfin.Api.Models.Entities
{
    public class WorkerExtensionStatusTypes
    {
        public int Id { get; set; }

        public string? StatusTypeDesc { get; set; }

        public bool IsActive { get; set; }
    }
}
