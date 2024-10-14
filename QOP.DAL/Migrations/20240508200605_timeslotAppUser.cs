using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QOP.DAL.Migrations
{
    /// <inheritdoc />
    public partial class timeslotAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "TimeSlots");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "TimeSlots",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_ApplicationUserId",
                table: "TimeSlots",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_AspNetUsers_ApplicationUserId",
                table: "TimeSlots",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_AspNetUsers_ApplicationUserId",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_ApplicationUserId",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "TimeSlots");

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "TimeSlots",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
