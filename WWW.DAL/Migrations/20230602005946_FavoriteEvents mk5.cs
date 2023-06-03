using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWW.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteEventsmk5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_AutorId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AutorId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "Articles");

            migrationBuilder.CreateTable(
                name: "ArticleUser",
                columns: table => new
                {
                    UserFavoriteEventsId = table.Column<int>(type: "int", nullable: false),
                    UserFavoriteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleUser", x => new { x.UserFavoriteEventsId, x.UserFavoriteId });
                    table.ForeignKey(
                        name: "FK_ArticleUser_Articles_UserFavoriteEventsId",
                        column: x => x.UserFavoriteEventsId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleUser_Users_UserFavoriteId",
                        column: x => x.UserFavoriteId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleUser_UserFavoriteId",
                table: "ArticleUser",
                column: "UserFavoriteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleUser");

            migrationBuilder.AddColumn<int>(
                name: "AutorId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AutorId",
                table: "Articles",
                column: "AutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_AutorId",
                table: "Articles",
                column: "AutorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
