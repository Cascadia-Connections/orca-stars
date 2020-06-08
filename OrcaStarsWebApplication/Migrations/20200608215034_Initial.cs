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
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    StoreFront = table.Column<string>(nullable: true),
                    ContactId = table.Column<Guid>(nullable: false),
                    Social = table.Column<Guid>(nullable: false),
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
                    BusinessId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hours", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Hours_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessContactId",
                table: "Businesses",
                column: "BusinessContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Hours_BusinessId",
                table: "Hours",
                column: "BusinessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hours");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
