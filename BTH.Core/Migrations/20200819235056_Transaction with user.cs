using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BTH.Core.Migrations
{
    public partial class Transactionwithuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoBaUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    ClientAccount = table.Column<string>(nullable: true),
                    BIC = table.Column<string>(nullable: true),
                    IBAN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoBaUsers", x => x.Id);
                });

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
                    Category = table.Column<string>(nullable: true),
                    UserAccountId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoBaTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoBaTransactions_CoBaUsers_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "CoBaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoBaTransactions_BookingText",
                table: "CoBaTransactions",
                column: "BookingText",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoBaTransactions_UserAccountId",
                table: "CoBaTransactions",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CoBaUsers_IBAN",
                table: "CoBaUsers",
                column: "IBAN",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoBaTransactions");

            migrationBuilder.DropTable(
                name: "CoBaUsers");
        }
    }
}
