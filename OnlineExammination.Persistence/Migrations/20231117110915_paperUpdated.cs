using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExammination.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class paperUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Papers_Programs_Program",
                table: "Papers");

            migrationBuilder.DropForeignKey(
                name: "FK_Papers_Semesters_SemesterId",
                table: "Papers");

            migrationBuilder.DropIndex(
                name: "IX_Papers_Program",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Program",
                table: "Papers");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "Papers",
                newName: "ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_Papers_SemesterId",
                table: "Papers",
                newName: "IX_Papers_ExamId");

            migrationBuilder.AlterColumn<int>(
                name: "CorrectOption",
                table: "Papers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Papers_Exams_ExamId",
                table: "Papers",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Papers_Exams_ExamId",
                table: "Papers");

            migrationBuilder.RenameColumn(
                name: "ExamId",
                table: "Papers",
                newName: "SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_Papers_ExamId",
                table: "Papers",
                newName: "IX_Papers_SemesterId");

            migrationBuilder.AlterColumn<string>(
                name: "CorrectOption",
                table: "Papers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "Program",
                table: "Papers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Papers_Program",
                table: "Papers",
                column: "Program");

            migrationBuilder.AddForeignKey(
                name: "FK_Papers_Programs_Program",
                table: "Papers",
                column: "Program",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Papers_Semesters_SemesterId",
                table: "Papers",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
