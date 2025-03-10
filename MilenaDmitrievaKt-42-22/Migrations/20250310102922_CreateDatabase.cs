using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilenaDmitrievaKt_42_22.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cafedra",
                columns: table => new
                {
                    Cafedra_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cafedra_Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Head_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Cafedra_cafedra_id", x => x.Cafedra_ID);
                });

            migrationBuilder.CreateTable(
                name: "Degree",
                columns: table => new
                {
                    Degree_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree_Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Degree_degree_id", x => x.Degree_ID);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Position_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position_Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Position_position_id", x => x.Position_ID);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Subject_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject_Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Subject_subject_id", x => x.Subject_ID);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Teacher_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Patronym = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Cafedra_ID = table.Column<int>(type: "int", nullable: false),
                    Degree_ID = table.Column<int>(type: "int", nullable: false),
                    Position_ID = table.Column<int>(type: "int", nullable: false),
                    HCafedraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Teacher_teacher_id", x => x.Teacher_ID);
                    table.ForeignKey(
                        name: "fk_cafedra_id",
                        column: x => x.Cafedra_ID,
                        principalTable: "Cafedra",
                        principalColumn: "Cafedra_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_degree_id",
                        column: x => x.Degree_ID,
                        principalTable: "Degree",
                        principalColumn: "Degree_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_head_id",
                        column: x => x.HCafedraId,
                        principalTable: "Cafedra",
                        principalColumn: "Cafedra_ID");
                    table.ForeignKey(
                        name: "fk_position_id",
                        column: x => x.Position_ID,
                        principalTable: "Position",
                        principalColumn: "Position_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Lessons_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Teacher_ID = table.Column<int>(type: "int", nullable: false),
                    Subject_ID = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Lessons_lessons_id", x => x.Lessons_ID);
                    table.ForeignKey(
                        name: "fk_subject_id",
                        column: x => x.Subject_ID,
                        principalTable: "Subject",
                        principalColumn: "Subject_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_teacher_id",
                        column: x => x.Teacher_ID,
                        principalTable: "Teacher",
                        principalColumn: "Teacher_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_Cafedra_fk_head_id",
                table: "Cafedra",
                column: "Head_ID");

            migrationBuilder.CreateIndex(
                name: "idx_Lessons_fk_subject_id",
                table: "Lessons",
                column: "Subject_ID");

            migrationBuilder.CreateIndex(
                name: "idx_Lessons_fk_teacher_id",
                table: "Lessons",
                column: "Teacher_ID");

            migrationBuilder.CreateIndex(
                name: "idx_Teacher_fk_cafedra_id",
                table: "Teacher",
                column: "Cafedra_ID");

            migrationBuilder.CreateIndex(
                name: "idx_Teacher_fk_degree_id",
                table: "Teacher",
                column: "Degree_ID");

            migrationBuilder.CreateIndex(
                name: "idx_Teacher_fk_position_id",
                table: "Teacher",
                column: "Position_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_HCafedraId",
                table: "Teacher",
                column: "HCafedraId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Cafedra");

            migrationBuilder.DropTable(
                name: "Degree");

            migrationBuilder.DropTable(
                name: "Position");
        }
    }
}
