using Microsoft.EntityFrameworkCore.Migrations;

namespace SP18.PF.Web.Migrations
{
    public partial class AddsRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "User",
                nullable: true);

            //we are making everone an admin since it is safer that way for now
            migrationBuilder.Sql("UPDATE [User] SET Role = 'Admin'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");
        }
    }
}
