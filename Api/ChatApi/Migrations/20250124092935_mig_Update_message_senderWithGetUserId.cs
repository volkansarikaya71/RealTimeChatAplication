using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApi.Migrations
{
    /// <inheritdoc />
    public partial class mig_Add_message_table4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_messages_ReceiverId",
                table: "messages",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_Users_ReceiverId",
                table: "messages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_Users_ReceiverId",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_ReceiverId",
                table: "messages");
        }
    }
}
