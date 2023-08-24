using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cukiernia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class wykluczonoUsuwanieKaskadowe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductQuantities_SubProducts_SubProductId",
                table: "ProductQuantities");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_Measures_MeasureId",
                table: "SubProducts");

            migrationBuilder.AlterColumn<int>(
                name: "SubProductQuantity",
                table: "ProductQuantities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SubProductId",
                table: "ProductQuantities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductQuantities_SubProducts_SubProductId",
                table: "ProductQuantities",
                column: "SubProductId",
                principalTable: "SubProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_Measures_MeasureId",
                table: "SubProducts",
                column: "MeasureId",
                principalTable: "Measures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductQuantities_SubProducts_SubProductId",
                table: "ProductQuantities");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_Measures_MeasureId",
                table: "SubProducts");

            migrationBuilder.AlterColumn<int>(
                name: "SubProductQuantity",
                table: "ProductQuantities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubProductId",
                table: "ProductQuantities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductQuantities_SubProducts_SubProductId",
                table: "ProductQuantities",
                column: "SubProductId",
                principalTable: "SubProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_Measures_MeasureId",
                table: "SubProducts",
                column: "MeasureId",
                principalTable: "Measures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
