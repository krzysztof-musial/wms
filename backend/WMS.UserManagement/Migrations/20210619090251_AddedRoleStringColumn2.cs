using Microsoft.EntityFrameworkCore.Migrations;

namespace WMS.UserManagement.Migrations
{
    public partial class AddedRoleStringColumn2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "roleKind",
                table: "user",
                newName: "RoleKind");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleKind",
                table: "user",
                newName: "roleKind");
        }
    }
}
