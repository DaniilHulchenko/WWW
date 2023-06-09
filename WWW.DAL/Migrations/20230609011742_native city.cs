using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWW.DAL.Migrations
{
    /// <inheritdoc />
    public partial class nativecity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "User_Details",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User_Details",
                keyColumn: "UserID",
                keyValue: 1,
                column: "City",
                value: null);

            migrationBuilder.UpdateData(
                table: "User_Details",
                keyColumn: "UserID",
                keyValue: 2,
                column: "City",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "User_Details");
        }
    }
}
