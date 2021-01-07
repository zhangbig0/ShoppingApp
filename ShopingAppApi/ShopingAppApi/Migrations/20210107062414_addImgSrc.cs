using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppApi.Migrations
{
    public partial class addImgSrc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Role = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Account = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Password = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    NickName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    ShoppingBracketId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Account = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Password = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeliverAddress = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingBracket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingBracket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingBracket_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Class = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ImgSrc = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    OrderId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goods_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingBracketGoods",
                columns: table => new
                {
                    GoodsId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ShoppingBracketId = table.Column<Guid>(type: "char(36)", nullable: false),
                    BracketGoodsNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingBracketGoods", x => new { x.GoodsId, x.ShoppingBracketId });
                    table.ForeignKey(
                        name: "FK_ShoppingBracketGoods_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingBracketGoods_ShoppingBracket_ShoppingBracketId",
                        column: x => x.ShoppingBracketId,
                        principalTable: "ShoppingBracket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "OrderId", "Price", "Stock" },
                values: new object[] { new Guid("88b3bc96-37a5-4c66-ac81-70166dfcddcb"), "电器", null, "热水器", null, 300m, 200 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "OrderId", "Price", "Stock" },
                values: new object[] { new Guid("5032e888-5ee2-411d-997d-6da72a5a438d"), "电器", null, "冰箱", null, 270m, 800 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "OrderId", "Price", "Stock" },
                values: new object[] { new Guid("612d2e74-20d7-4e5e-84a1-b951b596eb85"), "电器", null, "TV", null, 300m, 800 });

            migrationBuilder.CreateIndex(
                name: "IX_Goods_OrderId",
                table: "Goods",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBracket_CustomerId",
                table: "ShoppingBracket",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBracketGoods_ShoppingBracketId",
                table: "ShoppingBracketGoods",
                column: "ShoppingBracketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUser");

            migrationBuilder.DropTable(
                name: "ShoppingBracketGoods");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "ShoppingBracket");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
