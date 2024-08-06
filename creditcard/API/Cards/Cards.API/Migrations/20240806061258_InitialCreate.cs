using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cards.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CardholderName = table.Column<string>(type: "varchar(60)", nullable: true),
                    CardNumber = table.Column<string>(type: "varchar(16)", nullable: true),
                    ExpiryMonth = table.Column<string>(type: "varchar(2)", nullable: false),
                    ExpiryYear = table.Column<string>(type: "varchar(2)", nullable: false),
                    CVC = table.Column<string>(type: "varchar(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
