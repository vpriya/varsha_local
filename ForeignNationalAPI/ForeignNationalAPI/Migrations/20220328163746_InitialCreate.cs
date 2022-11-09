using Microsoft.EntityFrameworkCore.Migrations;

namespace ForeignNationalAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FN_Details",
                columns: table => new
                {
                    FNDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lastname = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    FNemail = table.Column<string>(type: "nvarchar(80)", nullable: true),
                    FNgender = table.Column<string>(type: "nvarchar(7)", nullable: true),
                    DateOfBirth = table.Column<string>(type: "nvarchar(8)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FN_Details", x => x.FNDetailId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FN_Details");
        }
    }
}
