using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportTasksCalendar.Application.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExerciseGoalAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Goal",
                table: "Exercises",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Goal",
                table: "Exercises");
        }
    }
}
