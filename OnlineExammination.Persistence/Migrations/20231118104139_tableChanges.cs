using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExammination.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class tableChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Batch",
                table: "Papers");

            migrationBuilder.AddColumn<string>(
                name: "Batch",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Batch",
                table: "Exams");

            migrationBuilder.AddColumn<string>(
                name: "Batch",
                table: "Papers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
