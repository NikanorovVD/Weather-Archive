using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherRecords",
                columns: table => new
                {
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Temperature = table.Column<decimal>(type: "numeric", nullable: true),
                    RelativeHumidity = table.Column<int>(type: "integer", nullable: true),
                    Td = table.Column<decimal>(type: "numeric", nullable: true),
                    AtmosphericPressure = table.Column<int>(type: "integer", nullable: true),
                    WindDirection = table.Column<string>(type: "text", nullable: true),
                    WindSpeed = table.Column<int>(type: "integer", nullable: true),
                    CloudCover = table.Column<int>(type: "integer", nullable: true),
                    H = table.Column<int>(type: "integer", nullable: true),
                    VV = table.Column<string>(type: "text", nullable: true),
                    WeatherPhenomena = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherRecords", x => x.DateTime);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherRecords");
        }
    }
}
