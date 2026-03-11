using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteAt",
                schema: "MySchema",
                table: "Vouchers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "MySchema",
                table: "Vouchers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteAt",
                schema: "MySchema",
                table: "Reviews",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "MySchema",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteAt",
                schema: "MySchema",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "MySchema",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                schema: "MySchema",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "MySchema",
                table: "Reviews");
        }
    }
}
