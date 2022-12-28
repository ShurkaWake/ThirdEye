using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThirdEye.Back.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Changedperiodstorecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "RoomsStateHistories");

            migrationBuilder.DropColumn(
                name: "CurrentStateStartTime",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "RoomsStateHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "RoomsStateHistories");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "RoomsStateHistories",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentStateStartTime",
                table: "Rooms",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
