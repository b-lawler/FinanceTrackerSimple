using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleFinanceTracker.Core.Data.Migrations
{
    public partial class AddUsernameToAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Accounts");
        }
    }
}
