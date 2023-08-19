using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelAdmin.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Photo = table.Column<string>(type: "text", nullable: false),
                    Square = table.Column<string>(type: "text", nullable: false),
                    Persons_count = table.Column<int>(type: "integer", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginModels",
                columns: table => new
                {
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginModels", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TariffPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Categorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TariffAdmins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    TariffPlanId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffAdmins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TariffAdmins_Categorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TariffAdmins_TariffPlans_TariffPlanId",
                        column: x => x.TariffPlanId,
                        principalTable: "TariffPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    TariffId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_TariffAdmins_TariffId",
                        column: x => x.TariffId,
                        principalTable: "TariffAdmins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dates_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Dates_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderPays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    GuestCount = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    BookingNumber = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPays_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    GuestCount = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    BookingNumber = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorys",
                columns: new[] { "Id", "Name", "Path", "Persons_count", "Photo", "Square" },
                values: new object[,]
                {
                    { 1, "Стандарт", "Standard", 2, "img/room/room-s.jpeg", "20 кв.м" },
                    { 2, "Стандарт \"queen-size\"", "StandardBig", 2, "img/room/room-sb.jpg", "20 кв.м" },
                    { 3, "Комфорт", "StandardGood", 2, "img/room/room-si.jpeg", "25 кв.м" },
                    { 4, "Комфорт \"queen-size\"", "StandardGoodBig", 2, "img/room/room-sbi.jpeg", "25 кв.м" },
                    { 5, "Полулюкс", "SemiLuxury", 4, "img/room/room-pl.jpg", "32 кв.м" },
                    { 6, "Люкс", "Luxury", 4, "img/room/room-l.jpg", "46 кв.м" }
                });

            migrationBuilder.InsertData(
                table: "LoginModels",
                columns: new[] { "Email", "Password", "Role" },
                values: new object[,]
                {
                    { "admin@mail.ru", "$2a$11$iF0y3eNqew/YXRPpPCGE2.sh3jwtWMbcOihfGd8M0/OWvGGKrgKGK", 0 },
                    { "manager@mail.ru", "$2a$11$lT8u7BmSxbPNzH14fER8I.AvOBPsqtOPUdR.xzbO1YdIzIuoxi9bW", 1 }
                });

            migrationBuilder.InsertData(
                table: "TariffPlans",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Без питания" },
                    { 2, "Завтрак включён" },
                    { 3, "Полупансион" },
                    { 4, "Полный пансион" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "CategoryId", "Number" },
                values: new object[,]
                {
                    { 1, 1, 204 },
                    { 2, 2, 307 },
                    { 3, 3, 405 },
                    { 4, 4, 412 },
                    { 5, 5, 514 },
                    { 6, 6, 618 }
                });

            migrationBuilder.InsertData(
                table: "TariffAdmins",
                columns: new[] { "Id", "CategoryId", "Price", "TariffPlanId" },
                values: new object[,]
                {
                    { 1, 1, 3000, 1 },
                    { 2, 1, 3800, 2 },
                    { 3, 1, 4600, 3 },
                    { 4, 1, 5400, 4 },
                    { 5, 2, 3400, 1 },
                    { 6, 2, 4300, 2 },
                    { 7, 2, 5200, 3 },
                    { 8, 2, 6200, 4 },
                    { 9, 3, 4100, 1 },
                    { 10, 3, 5300, 2 },
                    { 11, 3, 6300, 3 },
                    { 12, 3, 7400, 4 },
                    { 13, 4, 4500, 1 },
                    { 14, 4, 5400, 2 },
                    { 15, 4, 6200, 3 },
                    { 16, 4, 7200, 4 },
                    { 17, 5, 5300, 1 },
                    { 18, 5, 6400, 2 },
                    { 19, 5, 7500, 3 },
                    { 20, 5, 8700, 4 },
                    { 21, 6, 6300, 1 },
                    { 22, 6, 7400, 2 },
                    { 23, 6, 8500, 3 },
                    { 24, 6, 9800, 4 }
                });

            migrationBuilder.InsertData(
                table: "Dates",
                columns: new[] { "Id", "ClientId", "End", "RoomId", "Start" },
                values: new object[,]
                {
                    { 1, null, null, 1, null },
                    { 2, null, null, 2, null },
                    { 3, null, null, 3, null },
                    { 4, null, null, 4, null },
                    { 5, null, null, 5, null },
                    { 6, null, null, 6, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_RoomId",
                table: "Clients",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TariffId",
                table: "Clients",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_Dates_ClientId",
                table: "Dates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Dates_RoomId",
                table: "Dates",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPays_ClientId",
                table: "OrderPays",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CategoryId",
                table: "Rooms",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffAdmins_CategoryId",
                table: "TariffAdmins",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffAdmins_TariffPlanId",
                table: "TariffAdmins",
                column: "TariffPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Dates");

            migrationBuilder.DropTable(
                name: "LoginModels");

            migrationBuilder.DropTable(
                name: "OrderPays");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "TariffAdmins");

            migrationBuilder.DropTable(
                name: "Categorys");

            migrationBuilder.DropTable(
                name: "TariffPlans");
        }
    }
}
