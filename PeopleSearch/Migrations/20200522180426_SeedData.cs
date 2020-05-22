using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeopleSearch.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    StreetAddress = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    ZipCode = table.Column<string>(nullable: false),
                    PhotoFile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    PersonID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interest_People_PersonID",
                        column: x => x.PersonID,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BirthDate", "City", "FirstName", "LastName", "PhotoFile", "State", "StreetAddress", "ZipCode" },
                values: new object[,]
                {
                    { 1, new DateTime(1963, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Los Angeles", "Quentin", "Tarantino", "assets/images/photos/Quentin.png", "CA", "234 Main Steet", "90005" },
                    { 2, new DateTime(1974, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beverly Hills", "Leonardo", "DiCaprio", "assets/images/photos/Leonardo.png", "CA", "24 Church Steet", "90012" },
                    { 3, new DateTime(1990, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "New York", "Margot", "Robbie", "assets/images/photos/Margot.png", "NY", "2346 3rd Ave, Apt. 78A", "10025" },
                    { 4, new DateTime(1964, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Long Beach", "Brad", "Pitt", "assets/images/photos/Brad.png", "CA", "11 S. Sunny Rd.", "90032" },
                    { 5, new DateTime(1940, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "New York", "Al", "Pachino", "assets/images/photos/Al.png", "NY", "45 Broadway St, Ste 300", "10046" }
                });

            migrationBuilder.InsertData(
                table: "Interest",
                columns: new[] { "Id", "Name", "PersonID" },
                values: new object[,]
                {
                    { 1, "Directing", 1 },
                    { 2, "Writing", 1 },
                    { 3, "Sailing", 2 },
                    { 4, "Playing", 2 },
                    { 5, "Hunting", 2 },
                    { 6, "Fashion", 3 },
                    { 7, "Biking", 4 },
                    { 8, "Martial Arts", 4 },
                    { 9, "Gambling", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interest_PersonID",
                table: "Interest",
                column: "PersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interest");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
