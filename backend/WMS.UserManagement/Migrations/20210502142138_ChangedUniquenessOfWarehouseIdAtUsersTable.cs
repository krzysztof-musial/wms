using Microsoft.EntityFrameworkCore.Migrations;

namespace WMS.UserManagement.Migrations
{
    public partial class ChangedUniquenessOfWarehouseIdAtUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_WarehouseId",
                table: "user");

            migrationBuilder.CreateIndex(
                name: "IX_user_WarehouseId",
                table: "user",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_WarehouseId",
                table: "user");

            migrationBuilder.CreateIndex(
                name: "IX_user_WarehouseId",
                table: "user",
                column: "WarehouseId",
                unique: true,
                filter: "[WarehouseId] IS NOT NULL");
        }
    }
}
