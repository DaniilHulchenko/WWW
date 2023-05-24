using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WWW.DAL.Migrations
{
    /// <inheritdoc />
    public partial class plus_start_accounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "Email", "NickName", "Role" },
                values: new object[,]
                {
                    { 1, null, "admin@gmail.com", "admin", 2 },
                    { 2, null, "ticketmaster@gmail.com", "TicketMaster", 1 }
                });

            migrationBuilder.InsertData(
                table: "User_Details",
                columns: new[] { "UserID", "Introdaction", "Password" },
                values: new object[,]
                {
                    { 1, "Admin Account", "03531ed23e58d474162aec45787f78c784ce246f8df573bf1f89b5d6f75b68f7" },
                    { 2, "TicketMaster Official Account", "240c213c2ef6246c471147df64587a22d7198bf540f520e5ac04e99a45fdb6a4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User_Details",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User_Details",
                keyColumn: "UserID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
