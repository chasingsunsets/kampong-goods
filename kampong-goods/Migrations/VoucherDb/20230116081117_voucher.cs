using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kampong_goods.Migrations.VoucherDb
{
    public partial class voucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    VoucherId = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    VoucherName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    VoucherDiscAmt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VoucherMinSpend = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VoucherCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    VoucherExpDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.VoucherId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vouchers");
        }
    }
}
