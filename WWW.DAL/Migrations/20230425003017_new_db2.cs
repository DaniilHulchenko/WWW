using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWW.DAL.Migrations
{
    /// <inheritdoc />
    public partial class new_db2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Location_LocationidLocation",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_AutorID",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "idLocation",
                table: "Location",
                newName: "LocationID");

            migrationBuilder.RenameColumn(
                name: "AutorID",
                table: "Articles",
                newName: "AutorId");

            migrationBuilder.RenameColumn(
                name: "LocationidLocation",
                table: "Articles",
                newName: "LocationID");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AutorID",
                table: "Articles",
                newName: "IX_Articles_AutorId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_LocationidLocation",
                table: "Articles",
                newName: "IX_Articles_LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Location_LocationID",
                table: "Articles",
                column: "LocationID",
                principalTable: "Location",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_AutorId",
                table: "Articles",
                column: "AutorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Location_LocationID",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_AutorId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "LocationID",
                table: "Location",
                newName: "idLocation");

            migrationBuilder.RenameColumn(
                name: "AutorId",
                table: "Articles",
                newName: "AutorID");

            migrationBuilder.RenameColumn(
                name: "LocationID",
                table: "Articles",
                newName: "LocationidLocation");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AutorId",
                table: "Articles",
                newName: "IX_Articles_AutorID");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_LocationID",
                table: "Articles",
                newName: "IX_Articles_LocationidLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Location_LocationidLocation",
                table: "Articles",
                column: "LocationidLocation",
                principalTable: "Location",
                principalColumn: "idLocation",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_AutorID",
                table: "Articles",
                column: "AutorID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
