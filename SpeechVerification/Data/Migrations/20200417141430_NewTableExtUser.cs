using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeechVerification.Data.Migrations
{
    public partial class NewTableExtUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtUser",
                columns: table => new
                {
                    ProfileId = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtUser", x => x.ProfileId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtUser");
        }
    }
}
