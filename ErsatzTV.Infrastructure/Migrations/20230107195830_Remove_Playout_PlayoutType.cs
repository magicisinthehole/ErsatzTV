using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErsatzTV.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovePlayoutPlayoutType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgramSchedulePlayoutType",
                table: "Playout");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgramSchedulePlayoutType",
                table: "Playout",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
