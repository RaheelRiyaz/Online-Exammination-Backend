using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExammination.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class resultTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    ResultStatus = table.Column<int>(type: "int", nullable: false),
                    IsReleased = table.Column<int>(type: "int", nullable: false),
                    CeatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Users_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_EntityId",
                table: "Results",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ExamId",
                table: "Results",
                column: "ExamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");
        }
    }
}
