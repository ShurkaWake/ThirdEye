using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThirdEye.Back.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    InstitutionLocatedId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Institutions_InstitutionLocatedId",
                        column: x => x.InstitutionLocatedId,
                        principalTable: "Institutions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkerAccountId = table.Column<string>(type: "TEXT", nullable: true),
                    JobId = table.Column<int>(type: "INTEGER", nullable: true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdentityRole",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Workers_Institutions_JobId",
                        column: x => x.JobId,
                        principalTable: "Institutions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Workers_Users_WorkerAccountId",
                        column: x => x.WorkerAccountId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SerialNumber = table.Column<string>(type: "TEXT", nullable: true),
                    LastState = table.Column<int>(type: "INTEGER", nullable: false),
                    InstalationRoomId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Rooms_InstalationRoomId",
                        column: x => x.InstalationRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoomsStateHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChangeTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    RoomChangedId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomsStateHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomsStateHistories_Rooms_RoomChangedId",
                        column: x => x.RoomChangedId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_InstalationRoomId",
                table: "Devices",
                column: "InstalationRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_InstitutionLocatedId",
                table: "Rooms",
                column: "InstitutionLocatedId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomsStateHistories_RoomChangedId",
                table: "RoomsStateHistories",
                column: "RoomChangedId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_JobId",
                table: "Workers",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_RoleId",
                table: "Workers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_WorkerAccountId",
                table: "Workers",
                column: "WorkerAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "RoomsStateHistories");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Institutions");
        }
    }
}
