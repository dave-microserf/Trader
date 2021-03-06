﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Czarnikow.Trader.Infrastructure.Db.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Counterparty",
                columns: table => new
                {
                    CounterpartyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counterparty", x => x.CounterpartyId);
                    table.UniqueConstraint("AK_Counterparty_Name", x => x.Name);
                    table.CheckConstraint("CK_Name", "LEN(Name) > 0");
                });

            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    TradeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CounterpartyId = table.Column<int>(nullable: false),
                    Product = table.Column<string>(maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Direction = table.Column<string>(type: "CHAR(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.TradeId);
                    table.CheckConstraint("CK_Product", "LEN(Product) > 0");
                    table.CheckConstraint("CK_Quantity", "Quantity >= 0");
                    table.CheckConstraint("CK_Price", "Price >= 0");
                    table.CheckConstraint("CK_Direction", "Direction = 'B' OR Direction = 'S'");
                    table.ForeignKey(
                        name: "FK_Trade_Counterparty_CounterpartyId",
                        column: x => x.CounterpartyId,
                        principalTable: "Counterparty",
                        principalColumn: "CounterpartyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Counterparty",
                columns: new[] { "CounterpartyId", "Name" },
                values: new object[] { 1, "Company A" });

            migrationBuilder.InsertData(
                table: "Counterparty",
                columns: new[] { "CounterpartyId", "Name" },
                values: new object[] { 2, "Company B" });

            migrationBuilder.InsertData(
                table: "Trade",
                columns: new[] { "TradeId", "CounterpartyId", "Date", "Direction", "Price", "Product", "Quantity" },
                values: new object[] { 1, 1, new DateTime(2018, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", 400.50m, "Sugar", 100 });

            migrationBuilder.InsertData(
                table: "Trade",
                columns: new[] { "TradeId", "CounterpartyId", "Date", "Direction", "Price", "Product", "Quantity" },
                values: new object[] { 2, 2, new DateTime(2018, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "S", 450.10m, "Sugar", 100 });

            migrationBuilder.CreateIndex(
                name: "IX_Trade_CounterpartyId",
                table: "Trade",
                column: "CounterpartyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trade");

            migrationBuilder.DropTable(
                name: "Counterparty");
        }
    }
}
