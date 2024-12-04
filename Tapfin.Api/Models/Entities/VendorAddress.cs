using System.ComponentModel.DataAnnotations.Schema;

namespace Tapfin.Api.Models.Entities
{
    public class VendorAddress
    {
        public int Id { get; set; }

        public int VendorId { get; set; }

        public string Location { get; set; }

        public int ZipCode { get; set; }

        public string? Street { get; set; }

        public int? Number { get; set; }

        public string? Neighbourhood { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string? Complement { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public bool? IsActive { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorDetail VendorDetails { get; set; }
    }
}
