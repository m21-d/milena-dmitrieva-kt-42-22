using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilenaDmitrievaKt_42_22.Migrations
{
    /// <inheritdoc />
    public partial class CycleFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teacher_HCafedraId",
                table: "Teacher");

            migrationBuilder.AlterColumn<int>(
                name: "HCafedraId",
                table: "Teacher",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Head_ID",
                table: "Cafedra",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_HCafedraId",
                table: "Teacher",
                column: "HCafedraId",
                unique: true,
                filter: "[HCafedraId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teacher_HCafedraId",
                table: "Teacher");

            migrationBuilder.AlterColumn<int>(
                name: "HCafedraId",
                table: "Teacher",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Head_ID",
                table: "Cafedra",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_HCafedraId",
                table: "Teacher",
                column: "HCafedraId",
                unique: true);
        }
    }
}
