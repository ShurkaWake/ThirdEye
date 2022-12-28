using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThirdEye.Back.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedWayToStoreRoomStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Rooms_InstalationRoomId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Businesses_BusinessLocatedId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomsStateHistories_Rooms_RoomChangedId",
                table: "RoomsStateHistories");

            migrationBuilder.DropIndex(
                name: "IX_RoomsStateHistories_RoomChangedId",
                table: "RoomsStateHistories");

            migrationBuilder.DropColumn(
                name: "RoomChangedId",
                table: "RoomsStateHistories");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "RoomsStateHistories",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "ChangeTime",
                table: "RoomsStateHistories",
                newName: "StartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "RoomsStateHistories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rooms",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BusinessLocatedId",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentState",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Devices",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstalationRoomId",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Businesses",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomsStateHistories_RoomId",
                table: "RoomsStateHistories",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Rooms_InstalationRoomId",
                table: "Devices",
                column: "InstalationRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Businesses_BusinessLocatedId",
                table: "Rooms",
                column: "BusinessLocatedId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomsStateHistories_Rooms_RoomId",
                table: "RoomsStateHistories",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Rooms_InstalationRoomId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Businesses_BusinessLocatedId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomsStateHistories_Rooms_RoomId",
                table: "RoomsStateHistories");

            migrationBuilder.DropIndex(
                name: "IX_RoomsStateHistories_RoomId",
                table: "RoomsStateHistories");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "RoomsStateHistories");

            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "RoomsStateHistories",
                newName: "ChangeTime");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "RoomsStateHistories",
                newName: "State");

            migrationBuilder.AddColumn<int>(
                name: "RoomChangedId",
                table: "RoomsStateHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rooms",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessLocatedId",
                table: "Rooms",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Devices",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "InstalationRoomId",
                table: "Devices",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Businesses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_RoomsStateHistories_RoomChangedId",
                table: "RoomsStateHistories",
                column: "RoomChangedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Rooms_InstalationRoomId",
                table: "Devices",
                column: "InstalationRoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Businesses_BusinessLocatedId",
                table: "Rooms",
                column: "BusinessLocatedId",
                principalTable: "Businesses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomsStateHistories_Rooms_RoomChangedId",
                table: "RoomsStateHistories",
                column: "RoomChangedId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
