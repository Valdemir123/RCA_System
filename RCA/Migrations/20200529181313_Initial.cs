using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RCA.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatusId = table.Column<int>(nullable: false),
                    Channel_GroupLevelId = table.Column<int>(nullable: false),
                    Channel_GroupLevelItemId = table.Column<int>(nullable: false),
                    Channel_Code = table.Column<string>(maxLength: 50, nullable: true),
                    Channel_Tax = table.Column<double>(nullable: false),
                    Channel_Percent = table.Column<double>(nullable: false),
                    Book_GroupLevelItemId = table.Column<int>(nullable: false),
                    Book_GroupLevelItemTaxId = table.Column<int>(nullable: false),
                    Book_DateIn = table.Column<DateTime>(nullable: false),
                    Book_DateOut = table.Column<DateTime>(nullable: false),
                    Book_AdultsNum = table.Column<int>(nullable: false),
                    Book_KidsNum = table.Column<int>(nullable: false),
                    Book_PCD = table.Column<bool>(nullable: false),
                    Book_PET = table.Column<bool>(nullable: false),
                    Book_DayValue = table.Column<double>(nullable: false),
                    Book_DiscountPercent = table.Column<int>(nullable: false),
                    Book_InputValue = table.Column<double>(nullable: false),
                    GuestCPF = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatusId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    CNPJ = table.Column<string>(maxLength: 20, nullable: false),
                    ContactName = table.Column<string>(maxLength: 50, nullable: false),
                    Phone1 = table.Column<string>(maxLength: 20, nullable: false),
                    Phone2 = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: false),
                    Complement = table.Column<string>(maxLength: 50, nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: false),
                    State = table.Column<string>(maxLength: 50, nullable: false),
                    Country = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupLevel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatusId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    CPF = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Phone1 = table.Column<string>(maxLength: 20, nullable: false),
                    Phone2 = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: false),
                    Complement = table.Column<string>(maxLength: 50, nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: false),
                    State = table.Column<string>(maxLength: 50, nullable: false),
                    Country = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "GroupLevelItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatusId = table.Column<int>(nullable: false),
                    GroupLevelId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    OccupantsNum = table.Column<int>(nullable: false),
                    PCD = table.Column<bool>(nullable: false),
                    Class_GroupLevelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupLevelItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupLevelItem_GroupLevel_Class_GroupLevelId",
                        column: x => x.Class_GroupLevelId,
                        principalTable: "GroupLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupLevelItemTax",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GroupLevelItemId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    Percent = table.Column<int>(nullable: false),
                    Class_GroupLevelItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupLevelItemTax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupLevelItemTax_GroupLevelItem_Class_GroupLevelItemId",
                        column: x => x.Class_GroupLevelItemId,
                        principalTable: "GroupLevelItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupLevelItem_Class_GroupLevelId",
                table: "GroupLevelItem",
                column: "Class_GroupLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupLevelItemTax_Class_GroupLevelItemId",
                table: "GroupLevelItemTax",
                column: "Class_GroupLevelItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "GroupLevelItemTax");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropTable(
                name: "GroupLevelItem");

            migrationBuilder.DropTable(
                name: "GroupLevel");
        }
    }
}
