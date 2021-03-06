﻿using System;
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
                name: "Hours",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SunO = table.Column<string>(nullable: true),
                    SunC = table.Column<string>(nullable: true),
                    MonO = table.Column<string>(nullable: true),
                    MonC = table.Column<string>(nullable: true),
                    TuesO = table.Column<string>(nullable: true),
                    TuesC = table.Column<string>(nullable: true),
                    WedO = table.Column<string>(nullable: true),
                    WedC = table.Column<string>(nullable: true),
                    ThursO = table.Column<string>(nullable: true),
                    ThursC = table.Column<string>(nullable: true),
                    FriO = table.Column<string>(nullable: true),
                    FriC = table.Column<string>(nullable: true),
                    SatO = table.Column<string>(nullable: true),
                    SatC = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hours", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Facebook = table.Column<string>(nullable: true),
                    Twitter = table.Column<string>(nullable: true),
                    Instagram = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.ID);
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
                    Hours = table.Column<Guid>(nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessContactId",
                table: "Businesses",
                column: "BusinessContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "Hours");

            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
