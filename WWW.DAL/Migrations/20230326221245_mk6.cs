using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWW.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mk6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_AuthorId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "DateOfModification",
                table: "Articles",
                newName: "DateOfEvent");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Articles",
                newName: "AutorID");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                newName: "IX_Articles_AutorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_AutorID",
                table: "Articles",
                column: "AutorID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_AutorID",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "DateOfEvent",
                table: "Articles",
                newName: "DateOfModification");

            migrationBuilder.RenameColumn(
                name: "AutorID",
                table: "Articles",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AutorID",
                table: "Articles",
                newName: "IX_Articles_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
