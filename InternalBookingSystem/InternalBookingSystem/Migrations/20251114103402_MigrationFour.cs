using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternalBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class MigrationFour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmplyeePhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmplyeePhoneNumber",
                table: "AspNetUsers");
        }
    }
}
