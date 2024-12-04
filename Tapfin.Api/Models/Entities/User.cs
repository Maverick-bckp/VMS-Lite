using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tapfin.Api.Models.Entities
{
    public class User : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SId { get; set; }
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
        public string?  UserCustomId { get; set; }
        public int? CountryId { get; set; }
        public int? ClientId { get; set; }
        public int? VendorId { get; set; }
        public int? DepartmentId { get; set; }
        public string? Telephone { get; set; }
        public string? Cellphone { get; set; }
        public string? Observation { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public virtual Country? Country { get; set; } 
        public virtual ClientDetail? Client { get; set; } 
        public virtual VendorDetail? Vendor { get; set; }
    }
}
