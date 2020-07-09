using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BHT.WPF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoBaTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookingDate = table.Column<DateTime>(nullable: false),
                    ValueDate = table.Column<DateTime>(nullable: false),
                    TurnoverType = table.Column<string>(nullable: true),
                    BookingText = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    ClientAccount = table.Column<string>(nullable: true),
                    BIC = table.Column<string>(nullable: true),
                    IBAN = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoBaTransactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoBaTransactions");
        }
    }
}
