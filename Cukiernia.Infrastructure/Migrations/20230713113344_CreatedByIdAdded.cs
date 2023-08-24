using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cukiernia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedByIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Bakings",
                type: "nvarchar(450)",
                nullable: true);



            migrationBuilder.CreateIndex(
                name: "IX_Bakings_CreatedById",
                table: "Bakings",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Bakings_AspNetUsers_CreatedById",
                table: "Bakings",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bakings_AspNetUsers_CreatedById",
                table: "Bakings");

            migrationBuilder.DropIndex(
                name: "IX_Bakings_CreatedById",
                table: "Bakings");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Bakings");

        }
    }
}
