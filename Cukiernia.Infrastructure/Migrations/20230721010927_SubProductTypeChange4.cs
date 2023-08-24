using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cukiernia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubProductTypeChange4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeletable",
                table: "SubProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeletable",
                table: "SubProducts");
        }
    }
}
