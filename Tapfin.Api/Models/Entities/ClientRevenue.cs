using System.ComponentModel.DataAnnotations.Schema;

namespace Tapfin.Api.Models.Entities
{
    public class ClientRevenue
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public double ServiceFee { get; set; }

        public string BusinessTerms { get; set; }

        public DateTime Date { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public bool? IsActive { get; set; }

        [ForeignKey("ClientId")]
        public virtual ClientDetail ClientDetail { get; set; } = null!;
    }
}
