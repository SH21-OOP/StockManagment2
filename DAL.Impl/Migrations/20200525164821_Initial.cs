using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DAL.Impl.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComercialProductGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    PurchasePrice = table.Column<int>(nullable: false),
                    SellPrice = table.Column<int>(nullable: false),
                    DeliveryTime = table.Column<int>(nullable: false),
                    TermOfUse = table.Column<int>(nullable: false),
                    Ends = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComercialProductGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ComercialProductGroupID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Preparation = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ComercialProductGroup_ComercialProductGroupID",
                        column: x => x.ComercialProductGroupID,
                        principalTable: "ComercialProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ComercialProductGroupID",
                table: "Product",
                column: "ComercialProductGroupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ComercialProductGroup");
        }
    }
}
