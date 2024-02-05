using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExammination.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class examTableUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SemesterId",
                table: "Exams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SemesterId",
                table: "Exams",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Semesters_SemesterId",
                table: "Exams",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Semesters_SemesterId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_SemesterId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Exams");
        }
    }
}
