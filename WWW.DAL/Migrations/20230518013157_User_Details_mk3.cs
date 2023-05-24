using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWW.DAL.Migrations
{
    /// <inheritdoc />
    public partial class User_Details_mk3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Details_Users_User_ID",
                table: "User_Details");

            migrationBuilder.RenameColumn(
                name: "User_ID",
                table: "User_Details",
                newName: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Details_Users_UserID",
                table: "User_Details",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Details_Users_UserID",
                table: "User_Details");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "User_Details",
                newName: "User_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Details_Users_User_ID",
                table: "User_Details",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
