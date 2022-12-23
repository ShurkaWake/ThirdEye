using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThirdEye.Back.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RequiredFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomsStateHistories_Rooms_RoomChangedId",
                table: "RoomsStateHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_AspNetUsers_WorkerAccountId",
                table: "Workers");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Businesses_JobId",
                table: "Workers");

            migrationBuilder.AlterColumn<string>(
                name: "WorkerAccountId",
                table: "Workers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "Workers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomChangedId",
                table: "RoomsStateHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomsStateHistories_Rooms_RoomChangedId",
                table: "RoomsStateHistories",
                column: "RoomChangedId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_AspNetUsers_WorkerAccountId",
                table: "Workers",
                column: "WorkerAccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Businesses_JobId",
                table: "Workers",
                column: "JobId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomsStateHistories_Rooms_RoomChangedId",
                table: "RoomsStateHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_AspNetUsers_WorkerAccountId",
                table: "Workers");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Businesses_JobId",
                table: "Workers");

            migrationBuilder.AlterColumn<string>(
                name: "WorkerAccountId",
                table: "Workers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "Workers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RoomChangedId",
                table: "RoomsStateHistories",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomsStateHistories_Rooms_RoomChangedId",
                table: "RoomsStateHistories",
                column: "RoomChangedId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_AspNetUsers_WorkerAccountId",
                table: "Workers",
                column: "WorkerAccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Businesses_JobId",
                table: "Workers",
                column: "JobId",
                principalTable: "Businesses",
                principalColumn: "Id");
        }
    }
}
