using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppApi.Migrations
{
    public partial class bracketPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "ShoppingBracketGoods",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "ShoppingBracket",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "Price", "Stock" },
                values: new object[] { new Guid("786f21ac-dc8a-4a9e-bb13-ad991a104212"), "电器", null, "热水器", 300m, 200 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "Price", "Stock" },
                values: new object[] { new Guid("15060656-0d90-4be3-825a-9940100f04b6"), "电器", null, "冰箱", 270m, 800 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "ImgSrc", "Name", "Price", "Stock" },
                values: new object[] { new Guid("773be0cd-9624-4973-b0eb-f4184f5c2094"), "电器", null, "TV", 300m, 800 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("15060656-0d90-4be3-825a-9940100f04b6"));

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("773be0cd-9624-4973-b0eb-f4184f5c2094"));

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("786f21ac-dc8a-4a9e-bb13-ad991a104212"));

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ShoppingBracketGoods");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ShoppingBracket");

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
        }
    }
}
