using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class delIdDrivers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DriverLicense_UserId",
                table: "DriverLicense");

            migrationBuilder.DropColumn(
                name: "DriverLicenseId",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_DriverLicense_UserId",
                table: "DriverLicense",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DriverLicense_UserId",
                table: "DriverLicense");

            migrationBuilder.AddColumn<int>(
                name: "DriverLicenseId",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DriverLicense_UserId",
                table: "DriverLicense",
                column: "UserId",
                unique: true);
        }
    }
}
