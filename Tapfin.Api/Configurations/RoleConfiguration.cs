using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Id = "557de2e2-883c-482d-b677-a3718a220d4f",
                    ConcurrencyStamp = "557de2e2-883c-482d-b677-a3718a220d4f",
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin".ToUpper(),
                    Description = "Super Admin"
                },
                new Role
                {
                    Id = "2dd5b2b2-75b2-4b4e-8122-4677e896329b",
                    ConcurrencyStamp = "2dd5b2b2-75b2-4b4e-8122-4677e896329b",
                    Name = "Client AdminOps",
                    NormalizedName = "ClientAdminOps".ToUpper(),
                    Description = "Client AdminOps"
                }
            );
        }
    }
}
