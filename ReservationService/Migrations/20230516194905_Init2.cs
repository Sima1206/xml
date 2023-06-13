using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationService.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceForOneGuest",
                table: "Reservations");

            migrationBuilder.AddColumn<bool>(
                name: "PriceForOneGuest",
                table: "Accommodations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceForOneGuest",
                table: "Accommodations");

            migrationBuilder.AddColumn<bool>(
                name: "PriceForOneGuest",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
