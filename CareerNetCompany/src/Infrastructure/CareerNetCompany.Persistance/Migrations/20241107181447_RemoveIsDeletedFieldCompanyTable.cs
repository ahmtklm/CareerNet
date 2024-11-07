using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerNetCompany.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsDeletedFieldCompanyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Companies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Companies",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
