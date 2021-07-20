using System;
using System.Net;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Geolocation.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    Network = table.Column<ValueTuple<IPAddress, int>>(type: "cidr", nullable: false),
                    GeonameId = table.Column<int>(type: "integer", nullable: true),
                    RegisteredCountryGeonameId = table.Column<int>(type: "integer", nullable: true),
                    RepresentedCountryGeonameId = table.Column<string>(type: "text", nullable: true),
                    IsSatelliteProvider = table.Column<int>(type: "integer", nullable: true),
                    IsAnonymousProxy = table.Column<int>(type: "integer", nullable: true),
                    PostCode = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    AccuracyRadius = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.Network);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    GeonameId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocaleCode = table.Column<string>(type: "text", nullable: true),
                    ContinentCode = table.Column<string>(type: "text", nullable: true),
                    ContinentName = table.Column<string>(type: "text", nullable: true),
                    CountryCode = table.Column<string>(type: "text", nullable: true),
                    CountryName = table.Column<string>(type: "text", nullable: true),
                    Subdivision1IsoCode = table.Column<string>(type: "text", nullable: true),
                    District = table.Column<string>(type: "text", nullable: true),
                    Subdivision2IsoCode = table.Column<string>(type: "text", nullable: true),
                    Region = table.Column<string>(type: "text", nullable: true),
                    CityName = table.Column<string>(type: "text", nullable: true),
                    MetroCode = table.Column<string>(type: "text", nullable: true),
                    TimeZone = table.Column<string>(type: "text", nullable: true),
                    IsInEuropeanUnion = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.GeonameId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blocks");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
