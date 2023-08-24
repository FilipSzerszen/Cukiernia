using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cukiernia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubProductEncodedNameAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EncodedName",
                table: "SubProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncodedName",
                table: "SubProducts");
        }
    }
}
