using Microsoft.EntityFrameworkCore.Migrations;

namespace WMS.UserManagement.Migrations
{
    public partial class AddedRoleStringColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "roleKind",
                table: "user",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "roleKind",
                table: "user");
        }
    }
}
