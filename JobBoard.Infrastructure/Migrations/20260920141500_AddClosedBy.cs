using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClosedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClosedBy",
                table: "Jobs",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosedBy",
                table: "Jobs");
        }
    }
}
