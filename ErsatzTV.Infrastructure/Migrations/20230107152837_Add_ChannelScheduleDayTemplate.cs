using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErsatzTV.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChannelScheduleDayTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChannelScheduleDayTemplate",
                columns: table => new
                {
                    ChannelId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScheduleDayTemplateId = table.Column<int>(type: "INTEGER", nullable: false),
                    DaysOfWeek = table.Column<string>(type: "TEXT", nullable: true),
                    DaysOfMonth = table.Column<string>(type: "TEXT", nullable: true),
                    MonthsOfYear = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelScheduleDayTemplate", x => new { x.ChannelId, x.ScheduleDayTemplateId });
                    table.ForeignKey(
                        name: "FK_ChannelScheduleDayTemplate_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelScheduleDayTemplate_ScheduleDayTemplate_ScheduleDayTemplateId",
                        column: x => x.ScheduleDayTemplateId,
                        principalTable: "ScheduleDayTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelScheduleDayTemplate_ScheduleDayTemplateId",
                table: "ChannelScheduleDayTemplate",
                column: "ScheduleDayTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelScheduleDayTemplate");
        }
    }
}
