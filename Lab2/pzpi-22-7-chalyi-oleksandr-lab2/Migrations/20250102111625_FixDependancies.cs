using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLightSense.Migrations
{
    /// <inheritdoc />
    public partial class FixDependancies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceLogs_AlertId",
                table: "MaintenanceLogs",
                column: "AlertId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceLogs_Alerts_AlertId",
                table: "MaintenanceLogs",
                column: "AlertId",
                principalTable: "Alerts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceLogs_Alerts_AlertId",
                table: "MaintenanceLogs");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceLogs_AlertId",
                table: "MaintenanceLogs");
        }
    }
}
