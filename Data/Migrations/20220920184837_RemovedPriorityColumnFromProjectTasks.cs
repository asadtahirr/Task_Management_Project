using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_management_system.Data.Migrations
{
    public partial class RemovedPriorityColumnFromProjectTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "ProjectTasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "ProjectTasks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
