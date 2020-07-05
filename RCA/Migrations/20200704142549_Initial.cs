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
                    Book_GroupLevelItemTaxID = table.Column<int>(nullable: false),
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
                name: "Channel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatusId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    Percent = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatusId = table.Column<int>(nullable: false),
                    CNPJ = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Site = table.Column<string>(maxLength: 100, nullable: false),
                    ContactName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Phone1 = table.Column<string>(maxLength: 20, nullable: false),
                    Phone2 = table.Column<string>(maxLength: 20, nullable: true),
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
                name: "GroupLevelItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatusId = table.Column<int>(nullable: false),
                    GroupLevelId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    OccupantsNum = table.Column<int>(nullable: false),
                    PCD = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupLevelItem", x => x.Id);
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
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatusId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeasonItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SeasonId = table.Column<int>(nullable: false),
                    GroupLevelItemId = table.Column<int>(nullable: false),
                    Tax = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatusId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    TypeAccessId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: false),
                    UserName = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "GroupLevel");

            migrationBuilder.DropTable(
                name: "GroupLevelItem");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "SeasonItem");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
