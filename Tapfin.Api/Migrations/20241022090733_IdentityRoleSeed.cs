using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tapfin.Api.Migrations
{
    /// <inheritdoc />
    public partial class IdentityRoleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "job_details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    JobCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    JobTitle = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    JobDesc = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    JobLocation = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    NoOfPosition = table.Column<int>(type: "int", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: true),
                    ExpectedSalary = table.Column<double>(type: "float", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    JobStatus = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_job_clientID",
                        column: x => x.ClientId,
                        principalTable: "client_details",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_job_countryID",
                        column: x => x.CountryId,
                        principalTable: "country",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "job_statusType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusTypeDesc = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_statusType", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "vendor_details",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CountryId = table.Column<int>(type: "int", nullable: false),
            //        VendorCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
            //        VendorName = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
            //        CreatedBy = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
            //        UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
            //        UpdatedBy = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
            //        DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
            //        DeletedBy = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
            //        IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_vendor_details", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_vendor_countryID",
            //            column: x => x.CountryId,
            //            principalTable: "country",
            //            principalColumn: "Id");
            //    });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2dd5b2b2-75b2-4b4e-8122-4677e896329b", "2dd5b2b2-75b2-4b4e-8122-4677e896329b", "Client AdminOps", "Client AdminOps", "CLIENTADMINOPS" },
                    { "557de2e2-883c-482d-b677-a3718a220d4f", "557de2e2-883c-482d-b677-a3718a220d4f", "Super Admin", "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_job_details_ClientId",
                table: "job_details",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_job_details_CountryId",
                table: "job_details",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_vendor_details_CountryId",
                table: "vendor_details",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "job_details");

            migrationBuilder.DropTable(
                name: "job_statusType");

            //migrationBuilder.DropTable(
            //    name: "vendor_details");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2dd5b2b2-75b2-4b4e-8122-4677e896329b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "557de2e2-883c-482d-b677-a3718a220d4f");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetRoles");
        }
    }
}
