using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWW.DAL.Migrations
{
    /// <inheritdoc />
    public partial class User_Details_delete_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "User_Details");

            migrationBuilder.DropColumn(
                name: "RealName",
                table: "User_Details");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "User_Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RealName",
                table: "User_Details",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
