using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLightSense.Migrations
{
    /// <inheritdoc />
    public partial class AdjustDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Streetlights_Sensors_SensorId",
                table: "Streetlights");

            migrationBuilder.DropIndex(
                name: "IX_Streetlights_SensorId",
                table: "Streetlights");

            migrationBuilder.DropColumn(
                name: "SensorId",
                table: "Streetlights");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Sensors");

            migrationBuilder.AlterColumn<int>(
                name: "BrightnessLevel",
                table: "Streetlights",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Streetlights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StreetlightId",
                table: "Sensors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "StreetlightId",
                table: "MaintenanceLogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AlertId",
                table: "MaintenanceLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StreetlightId",
                table: "Alerts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SensorId",
                table: "Alerts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_StreetlightId",
                table: "Sensors",
                column: "StreetlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Streetlights_StreetlightId",
                table: "Sensors",
                column: "StreetlightId",
                principalTable: "Streetlights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Streetlights_StreetlightId",
                table: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_Sensors_StreetlightId",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Streetlights");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "StreetlightId",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "AlertId",
                table: "MaintenanceLogs");

            migrationBuilder.AlterColumn<string>(
                name: "BrightnessLevel",
                table: "Streetlights",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SensorId",
                table: "Streetlights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Sensors",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "StreetlightId",
                table: "MaintenanceLogs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StreetlightId",
                table: "Alerts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SensorId",
                table: "Alerts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Streetlights_SensorId",
                table: "Streetlights",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Streetlights_Sensors_SensorId",
                table: "Streetlights",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
