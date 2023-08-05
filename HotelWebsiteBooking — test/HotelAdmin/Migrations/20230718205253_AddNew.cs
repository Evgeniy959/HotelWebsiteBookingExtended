using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAdmin.Migrations
{
    /// <inheritdoc />
    public partial class AddNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPayables");

            migrationBuilder.CreateTable(
                name: "OrderPays",
                columns: table => new
                {
                    Number = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    GuestCount = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPays", x => x.Number);
                    table.ForeignKey(
                        name: "FK_OrderPays_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Email",
                keyValue: "admin@mail.ru",
                column: "Password",
                value: "$2a$11$rOCkPUBN/VBBK/cMyslZo.gIN7UJ/Y3Kba6l98id5FYuE/bnkr0D2");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPays_ClientId",
                table: "OrderPays",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPays");

            migrationBuilder.CreateTable(
                name: "OrderPayables",
                columns: table => new
                {
                    Number = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuestCount = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPayables", x => x.Number);
                    table.ForeignKey(
                        name: "FK_OrderPayables_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Email",
                keyValue: "admin@mail.ru",
                column: "Password",
                value: "$2a$11$Pbc0e.ap6pnWHYQDcrzZ/ejJnSYgAPa3ke/oEcc4cCOUIv5nv3VTa");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPayables_ClientId",
                table: "OrderPayables",
                column: "ClientId");
        }
    }
}
