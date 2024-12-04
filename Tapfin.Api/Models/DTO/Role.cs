using Microsoft.AspNetCore.Identity;

namespace Tapfin.Api.Models.DTO
{
    public class Role : IdentityRole
    {
        public string? Description { get; set; }
    }
}
