using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrcaStarsWebApplication.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hours",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Day = table.Column<string>(nullable: true),
                    OpenHour = table.Column<string>(nullable: true),
                    OpenMinute = table.Column<string>(nullable: true),
                    CloseHour = table.Column<string>(nullable: true),
                    CloseMinute = table.Column<string>(nullable: true),
                    HoursID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hours", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Hours_Hours_HoursID",
                        column: x => x.HoursID,
                        principalTable: "Hours",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedia",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Twitter = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    Youtube = table.Column<string>(nullable: true),
                    Instagram = table.Column<string>(nullable: true),
                    Other = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedia", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false),
                    SocialID = table.Column<Guid>(nullable: true),
                    HoursID = table.Column<Guid>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    StoreFront = table.Column<string>(nullable: true),
                    ContactId = table.Column<Guid>(nullable: false),
                    BusinessContactId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Businesses_Contacts_BusinessContactId",
                        column: x => x.BusinessContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Businesses_Hours_HoursID",
                        column: x => x.HoursID,
                        principalTable: "Hours",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Businesses_SocialMedia_SocialID",
                        column: x => x.SocialID,
                        principalTable: "SocialMedia",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessContactId",
                table: "Businesses",
                column: "BusinessContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_HoursID",
                table: "Businesses",
                column: "HoursID");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_SocialID",
                table: "Businesses",
                column: "SocialID");

            migrationBuilder.CreateIndex(
                name: "IX_Hours_HoursID",
                table: "Hours",
                column: "HoursID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Hours");

            migrationBuilder.DropTable(
                name: "SocialMedia");
        }
    }
}
