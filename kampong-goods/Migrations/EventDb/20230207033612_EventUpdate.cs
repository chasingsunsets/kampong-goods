using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kampong_goods.Migrations.EventDb
{
    public partial class EventUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EventDesc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    EventStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EventEndDate = table.Column<DateTime>(type: "date", nullable: false),
                    EventStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    EventSuitability = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    EventOrganiser = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
