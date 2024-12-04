using System.ComponentModel.DataAnnotations.Schema;

namespace Tapfin.Api.Models.Entities
{
    public class VendorBilling
    {
        public int Id { get; set; }

        public int VendorId { get; set; }

        public double ServiceFee { get; set; }

        public string CommercialTerms { get; set; }

        public DateTime Date { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public bool? IsActive { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorDetail? VendorDetail { get; set; }
    }
}
