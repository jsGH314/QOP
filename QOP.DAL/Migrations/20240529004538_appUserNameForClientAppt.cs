using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QOP.DAL.Migrations
{
    /// <inheritdoc />
    public partial class appUserNameForClientAppt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Appointments");
        }
    }
}
