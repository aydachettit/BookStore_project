using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SecondInitialHuyLuong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Books",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "4d64713b-a9f8-4ea9-a220-627221c40f5d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "ee4213b0-e502-4147-8541-982f87cadffe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c262291f-4d61-4e80-a179-853ec79d0c31", "AQAAAAEAACcQAAAAEADO73FKF0lYFsLZYjVQuRNCRRZSUcU9+ArUAbSq6mxBw/AgneZHSVbQ+jZvlYMYvA==", "59d7ba8d-495c-439a-a089-2e55bf91a08e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf17dde4-fc6b-44a6-bb0d-e7cad2e71e8b", "AQAAAAEAACcQAAAAED7Bv1BX3d8UHws679OShpiz3ICpqjKZLzmFqOkoTfQKteh7TajimzDhNu1z44FUAA==", "9c080d4f-15df-439a-8958-2dec14ab8922" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Books",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3df57c53-c632-48bd-803d-00586db9c5e3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "e3eac33c-0456-44d9-a1d8-4aac1d2f7d8a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "13cf3bf5-a5dc-4174-adcf-dadb587aa3a0", "AQAAAAEAACcQAAAAEEX3Tz2rwv/mdBeRMdWASuQoTuugNGre842i+E/+H8JbZMENA1AjS4KLxmsSWZ+v9Q==", "f6285a06-3901-4fc3-8049-bc3d69bec31d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb0079ba-e0ff-4660-840e-8c74e8391cfc", "AQAAAAEAACcQAAAAEOblon2aQgGOddMLTOK/FBCtIpZSM+Gc4bdKsS41O2ZXV19o6TQk/KLl3plpIjLROw==", "b985fc2d-70de-4a38-9543-1542a0f1b518" });
        }
    }
}
