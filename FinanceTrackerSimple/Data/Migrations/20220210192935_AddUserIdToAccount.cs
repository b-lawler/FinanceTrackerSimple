using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceTrackerSimple.Data.Migrations
{
    public partial class AddUserIdToAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Accounts");
        }
    }
}
