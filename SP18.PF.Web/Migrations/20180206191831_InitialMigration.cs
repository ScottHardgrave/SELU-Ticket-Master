using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SP18.PF.Web.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 64, nullable: true),
                    BillingAddress_AddressLine1 = table.Column<string>(maxLength: 64, nullable: true),
                    BillingAddress_AddressLine2 = table.Column<string>(maxLength: 64, nullable: true),
                    BillingAddress_City = table.Column<string>(maxLength: 64, nullable: true),
                    BillingAddress_State = table.Column<string>(maxLength: 2, nullable: true),
                    BillingAddress_ZipCode = table.Column<string>(maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Capacity = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    PhysicalAddress_AddressLine1 = table.Column<string>(maxLength: 64, nullable: true),
                    PhysicalAddress_AddressLine2 = table.Column<string>(maxLength: 64, nullable: true),
                    PhysicalAddress_City = table.Column<string>(maxLength: 64, nullable: true),
                    PhysicalAddress_State = table.Column<string>(maxLength: 2, nullable: true),
                    PhysicalAddress_ZipCode = table.Column<string>(maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    VenueId = table.Column<int>(nullable: false),
                    TourId = table.Column<int>(nullable: false),
                    EventEnd = table.Column<DateTimeOffset>(nullable: false),
                    EventStart = table.Column<DateTimeOffset>(nullable: false),
                    TicketPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => new { x.VenueId, x.TourId });
                    table.ForeignKey(
                        name: "FK_Event_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Venue_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventTourId = table.Column<int>(nullable: false),
                    EventVenueId = table.Column<int>(nullable: false),
                    PurchasePrice = table.Column<decimal>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Event_EventVenueId_EventTourId",
                        columns: x => new { x.EventVenueId, x.EventTourId },
                        principalTable: "Event",
                        principalColumns: new[] { "VenueId", "TourId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_TourId",
                table: "Event",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EventVenueId_EventTourId",
                table: "Ticket",
                columns: new[] { "EventVenueId", "EventTourId" });

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "Venue");
        }
    }
}
