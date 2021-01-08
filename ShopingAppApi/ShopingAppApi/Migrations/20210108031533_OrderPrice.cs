using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppApi.Migrations
{
    public partial class OrderPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Order_OrderId",
                table: "Goods");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Goods_OrderId",
                table: "Goods");

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("0e2b61f6-6acd-4f8b-9d79-96b506a939a0"));

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("6c9671fa-a3a6-47e6-b2dd-ebcb0805ad3f"));

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("e7cce350-600d-4174-99f3-1546511c9b80"));

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Goods");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Order",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GoodsId",
                table: "Order",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Order",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "Price", "Stock" },
                values: new object[] { new Guid("041fa847-a762-483c-a76a-34a0327e638e"), "电器", null, "热水器", 300m, 200 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "Price", "Stock" },
                values: new object[] { new Guid("a91c56b5-1f5c-4dc5-a395-92cd4855eff6"), "电器", null, "冰箱", 270m, 800 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "Price", "Stock" },
                values: new object[] { new Guid("4a2337b7-d669-4439-9105-46829bc8ca61"), "电器", null, "TV", 300m, 800 });

            migrationBuilder.CreateIndex(
                name: "IX_Order_GoodsId",
                table: "Order",
                column: "GoodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Goods_GoodsId",
                table: "Order",
                column: "GoodsId",
                principalTable: "Goods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Goods_GoodsId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_GoodsId",
                table: "Order");

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("041fa847-a762-483c-a76a-34a0327e638e"));

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("4a2337b7-d669-4439-9105-46829bc8ca61"));

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("a91c56b5-1f5c-4dc5-a395-92cd4855eff6"));

            migrationBuilder.DropColumn(
                name: "GoodsId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Order");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Order",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Goods",
                type: "char(36)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "OrderId", "Price", "Stock" },
                values: new object[] { new Guid("e7cce350-600d-4174-99f3-1546511c9b80"), "电器", null, "热水器", null, 300m, 200 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "OrderId", "Price", "Stock" },
                values: new object[] { new Guid("6c9671fa-a3a6-47e6-b2dd-ebcb0805ad3f"), "电器", null, "冰箱", null, 270m, 800 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "OrderId", "Price", "Stock" },
                values: new object[] { new Guid("0e2b61f6-6acd-4f8b-9d79-96b506a939a0"), "电器", null, "TV", null, 300m, 800 });

            migrationBuilder.CreateIndex(
                name: "IX_Goods_OrderId",
                table: "Goods",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_Order_OrderId",
                table: "Goods",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
