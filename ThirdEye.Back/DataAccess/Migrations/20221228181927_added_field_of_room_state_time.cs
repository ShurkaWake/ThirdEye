using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThirdEye.Back.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedfieldofroomstatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StateTimeSeconds",
                table: "RoomsStateHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateTimeSeconds",
                table: "RoomsStateHistories");
        }
    }
}
