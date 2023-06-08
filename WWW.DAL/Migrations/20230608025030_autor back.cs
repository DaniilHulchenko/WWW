using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWW.DAL.Migrations
{
    /// <inheritdoc />
    public partial class autorback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleUser_Articles_EventId",
                table: "ArticleUser");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "ArticleUser",
                newName: "FavEventId");

            migrationBuilder.AddColumn<int>(
                name: "AutorId",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AutorId",
                table: "Articles",
                column: "AutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_AutorId",
                table: "Articles",
                column: "AutorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleUser_Articles_FavEventId",
                table: "ArticleUser",
                column: "FavEventId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_AutorId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleUser_Articles_FavEventId",
                table: "ArticleUser");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AutorId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "FavEventId",
                table: "ArticleUser",
                newName: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleUser_Articles_EventId",
                table: "ArticleUser",
                column: "EventId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
