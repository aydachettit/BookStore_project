using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FirstInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDetail");

            migrationBuilder.AddColumn<string>(
                name: "employeeAddress",
                table: "Employee",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "employeeDOB",
                table: "Employee",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "employeeEmail",
                table: "Employee",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "employeeImage",
                table: "Employee",
                type: "longtext",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Customer_ID",
                table: "Bills",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e35fdb2a-b1de-4046-a3e5-cecaa985674a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "9346dd29-35d3-4187-b810-471a4b9a54e4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87a2e9c3-9f64-41d6-9da1-fa0b258bc9b7", "AQAAAAEAACcQAAAAEIFinDbi+0moDgQNi60a3NOnJp+WKR9z7LQhNkE641gFCl5BuqTLYCYlXIX8Ja8qWA==", "4053e60b-bc85-4361-a722-7724e882a56e" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2", 0, "199ddaf3-9f76-4480-9160-811983a3b646", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEIxxAxEPqx2PMkZeTThWaVNeJC0GHMN1a81UkEXMxMgmGcJfcMFFrFIn97FADvNwTg==", null, false, "26045904-f16e-4e83-96d7-15d637deec75", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DropColumn(
                name: "employeeAddress",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "employeeDOB",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "employeeEmail",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "employeeImage",
                table: "Employee");

            migrationBuilder.AlterColumn<int>(
                name: "Customer_ID",
                table: "Bills",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateTable(
                name: "ProductDetail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    ProductDescription = table.Column<string>(type: "longtext", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductTitle = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetail", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "372aef4f-38c1-4552-93d6-20cb879b4577");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "cf35a6f1-c1ed-4111-8ac8-faf277c6eb93");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04909683-ab04-4edb-8584-93bb8e9a6421", "AQAAAAEAACcQAAAAEE6TehhA+BSs4FTgbegrKqH5LfWhkzTKSropH6zCVLouiaJyQxQj0tqlNTUks+D+uQ==", "c7e99dcd-839a-43c9-bba4-b9d7955bd6ca" });
        }
    }
}
