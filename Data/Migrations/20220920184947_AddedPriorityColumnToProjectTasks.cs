using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_management_system.Data.Migrations
{
    public partial class AddedPriorityColumnToProjectTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "ProjectTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "ProjectTasks");
        }
    }
}
