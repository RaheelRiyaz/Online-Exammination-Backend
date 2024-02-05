using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExammination.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class moretableChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReleased",
                table: "Papers");

            migrationBuilder.AddColumn<bool>(
                name: "IsConducted",
                table: "Exams",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConducted",
                table: "Exams");

            migrationBuilder.AddColumn<bool>(
                name: "IsReleased",
                table: "Papers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
