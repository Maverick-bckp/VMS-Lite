using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tapfin.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSIDIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SId",
                table: "AspNetUsers");
        }
    }
}
