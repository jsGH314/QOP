using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QOP.DAL.Migrations
{
    /// <inheritdoc />
    public partial class staffTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "StaffMembers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "StaffMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
