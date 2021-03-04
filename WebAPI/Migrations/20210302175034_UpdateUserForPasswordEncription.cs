using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class UpdateUserForPasswordEncription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("password", "tbl_users");
            migrationBuilder.AddColumn<byte[]>(
                name: "password",
                table: "tbl_users",
                defaultValue: "admin@123",
                nullable: false
            );

            migrationBuilder.AddColumn<byte[]>(
                name: "password_key",
                table: "tbl_users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password_key",
                table: "tbl_users");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "tbl_users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]));
        }
    }
}
