using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWW.DAL.Migrations
{
    /// <inheritdoc />
    public partial class favoritemk6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleUser_Articles_UserFavoriteEventsId",
                table: "ArticleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleUser_Users_UserFavoriteId",
                table: "ArticleUser");

            migrationBuilder.RenameColumn(
                name: "UserFavoriteId",
                table: "ArticleUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserFavoriteEventsId",
                table: "ArticleUser",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleUser_UserFavoriteId",
                table: "ArticleUser",
                newName: "IX_ArticleUser_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleUser_Articles_EventId",
                table: "ArticleUser",
                column: "EventId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleUser_Users_UserId",
                table: "ArticleUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleUser_Articles_EventId",
                table: "ArticleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleUser_Users_UserId",
                table: "ArticleUser");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ArticleUser",
                newName: "UserFavoriteId");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "ArticleUser",
                newName: "UserFavoriteEventsId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleUser_UserId",
                table: "ArticleUser",
                newName: "IX_ArticleUser_UserFavoriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleUser_Articles_UserFavoriteEventsId",
                table: "ArticleUser",
                column: "UserFavoriteEventsId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleUser_Users_UserFavoriteId",
                table: "ArticleUser",
                column: "UserFavoriteId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
