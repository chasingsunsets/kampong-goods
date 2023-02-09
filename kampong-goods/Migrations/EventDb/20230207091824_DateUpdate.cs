using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kampong_goods.Migrations.EventDb
{
    public partial class DateUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventEndDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventStartDate",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "EventStartTime",
                table: "Events",
                newName: "EventStart");

            migrationBuilder.RenameColumn(
                name: "EventEndTime",
                table: "Events",
                newName: "EventEnd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventStart",
                table: "Events",
                newName: "EventStartTime");

            migrationBuilder.RenameColumn(
                name: "EventEnd",
                table: "Events",
                newName: "EventEndTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventEndDate",
                table: "Events",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EventStartDate",
                table: "Events",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
