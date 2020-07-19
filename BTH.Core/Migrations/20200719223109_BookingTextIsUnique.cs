using Microsoft.EntityFrameworkCore.Migrations;

namespace BHT.Core.Migrations
{
    public partial class BookingTextIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CoBaTransactions_BookingText",
                table: "CoBaTransactions",
                column: "BookingText",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CoBaTransactions_BookingText",
                table: "CoBaTransactions");
        }
    }
}
