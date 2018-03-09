using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SP18.PF.Web.Migrations
{
    public partial class AddsEventId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //since we are migrating changes that may break existing data - let's just nuke it because we are lazy
            migrationBuilder.Sql("DELETE FROM dbo.[Event]");
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Event_EventVenueId_EventTourId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_EventVenueId_EventTourId",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EventTourId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "EventVenueId",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Ticket",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Event",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EventId",
                table: "Ticket",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_VenueId",
                table: "Event",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Event_EventId",
                table: "Ticket",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Event_EventId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_EventId",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_VenueId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Event");

            migrationBuilder.AddColumn<int>(
                name: "EventTourId",
                table: "Ticket",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventVenueId",
                table: "Ticket",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                columns: new[] { "VenueId", "TourId" });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EventVenueId_EventTourId",
                table: "Ticket",
                columns: new[] { "EventVenueId", "EventTourId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Event_EventVenueId_EventTourId",
                table: "Ticket",
                columns: new[] { "EventVenueId", "EventTourId" },
                principalTable: "Event",
                principalColumns: new[] { "VenueId", "TourId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
