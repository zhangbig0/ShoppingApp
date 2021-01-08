using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppApi.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("1fb33a89-2c00-43c8-aadb-67468d30c39d"));

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("390c7b0f-1030-47d5-95c6-8a635a05a5bf"));

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("4990bb56-d7e1-48dd-a3f5-60b012dd37b8"));

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "OrderId", "Price", "Stock" },
                values: new object[] { new Guid("390c7b0f-1030-47d5-95c6-8a635a05a5bf"), "电器", null, "热水器", null, 300m, 200 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "OrderId", "Price", "Stock" },
                values: new object[] { new Guid("4990bb56-d7e1-48dd-a3f5-60b012dd37b8"), "电器", null, "冰箱", null, 270m, 800 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "OrderId", "Price", "Stock" },
                values: new object[] { new Guid("1fb33a89-2c00-43c8-aadb-67468d30c39d"), "电器", null, "TV", null, 300m, 800 });
        }
    }
}
