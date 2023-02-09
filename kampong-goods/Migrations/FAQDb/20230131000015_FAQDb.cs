using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kampong_goods.Migrations.FAQDb
{
    public partial class FAQDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FAQs",
                columns: table => new
                {
                    FAQId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Modified = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQs", x => x.FAQId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FAQs");
        }
    }
}

