using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smart_bus_verification.Migrations
{
    public partial class expenditureadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenditures",
                columns: table => new
                {
                    ExpenditureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    DateOfExpenditure = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiptImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiptReference = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenditures", x => x.ExpenditureID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenditures");
        }
    }
}
