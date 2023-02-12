using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kampong_goods.Migrations.FAQDb
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FAQCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FAQCatId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FAQCategoryName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FAQCatDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQCategorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQs",
                columns: table => new
                {
                    FAQId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FAQCategoryId = table.Column<int>(type: "int", nullable: true),
                    FAQCatId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Modified = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publish = table.Column<bool>(type: "bit", nullable: false),
                    ClickTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQs", x => x.FAQId);
                    table.ForeignKey(
                        name: "FK_FAQs_FAQCategorys_FAQCategoryId",
                        column: x => x.FAQCategoryId,
                        principalTable: "FAQCategorys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FAQs_FAQCategoryId",
                table: "FAQs",
                column: "FAQCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FAQs");

            migrationBuilder.DropTable(
                name: "FAQCategorys");
        }
    }
}
