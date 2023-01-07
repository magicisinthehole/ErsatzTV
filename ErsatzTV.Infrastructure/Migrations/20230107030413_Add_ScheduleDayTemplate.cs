using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErsatzTV.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduleDayTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleDayTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDayTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleDayTemplateItem",
                columns: table => new
                {
                    ScheduleDayTemplateId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScheduleBlockId = table.Column<int>(type: "INTEGER", nullable: false),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDayTemplateItem", x => new { x.ScheduleDayTemplateId, x.ScheduleBlockId });
                    table.ForeignKey(
                        name: "FK_ScheduleDayTemplateItem_ScheduleBlock_ScheduleBlockId",
                        column: x => x.ScheduleBlockId,
                        principalTable: "ScheduleBlock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleDayTemplateItem_ScheduleDayTemplate_ScheduleDayTemplateId",
                        column: x => x.ScheduleDayTemplateId,
                        principalTable: "ScheduleDayTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDayTemplateItem_ScheduleBlockId",
                table: "ScheduleDayTemplateItem",
                column: "ScheduleBlockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleDayTemplateItem");

            migrationBuilder.DropTable(
                name: "ScheduleDayTemplate");
        }
    }
}
