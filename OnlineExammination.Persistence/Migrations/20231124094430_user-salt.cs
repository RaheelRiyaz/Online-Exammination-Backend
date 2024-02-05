using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExammination.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class usersalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Programs_ProgramId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Semesters_SemesterId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Subjects_SubjectId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Papers_Exams_ExamId",
                table: "Papers");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Exams_ExamId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Users_EntityId",
                table: "Results");

            migrationBuilder.AddColumn<string>(
                name: "ResetCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Programs_ProgramId",
                table: "Exams",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Semesters_SemesterId",
                table: "Exams",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Subjects_SubjectId",
                table: "Exams",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Papers_Exams_ExamId",
                table: "Papers",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Exams_ExamId",
                table: "Results",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Users_EntityId",
                table: "Results",
                column: "EntityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Programs_ProgramId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Semesters_SemesterId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Subjects_SubjectId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Papers_Exams_ExamId",
                table: "Papers");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Exams_ExamId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Users_EntityId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "ResetCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Programs_ProgramId",
                table: "Exams",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Semesters_SemesterId",
                table: "Exams",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Subjects_SubjectId",
                table: "Exams",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Papers_Exams_ExamId",
                table: "Papers",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Exams_ExamId",
                table: "Results",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Users_EntityId",
                table: "Results",
                column: "EntityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
