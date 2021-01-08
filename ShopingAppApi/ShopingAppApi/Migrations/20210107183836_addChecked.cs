using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppApi.Migrations
{
    public partial class addChecked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("5032e888-5ee2-411d-997d-6da72a5a438d"));

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("612d2e74-20d7-4e5e-84a1-b951b596eb85"));

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("88b3bc96-37a5-4c66-ac81-70166dfcddcb"));

            migrationBuilder.AddColumn<bool>(
                name: "Checked",
                table: "ShoppingBracketGoods",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Checked",
                table: "ShoppingBracketGoods");

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
        }
    }
}
