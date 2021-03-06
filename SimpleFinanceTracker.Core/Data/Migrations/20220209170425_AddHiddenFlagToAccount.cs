using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleFinanceTracker.Core.Data.Migrations
{
    public partial class AddHiddenFlagToAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "Accounts");
        }
    }
}
