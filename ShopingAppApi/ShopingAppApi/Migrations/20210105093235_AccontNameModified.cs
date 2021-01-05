using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppApi.Migrations
{
    public partial class AccontNameModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("4284cb01-5786-4112-9ecc-8ddf0c5cd17d"));

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("81b4ab69-d459-47fa-81d6-58417ee5e7eb"));

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("add77178-3619-4cd5-aa54-f79ab42df1bc"));

            migrationBuilder.RenameColumn(
                name: "Accout",
                table: "Customer",
                newName: "Account");

            migrationBuilder.RenameColumn(
                name: "Accout",
                table: "AdminUser",
                newName: "Account");

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "Name", "OrderId", "Price", "ShoppingBracketId", "Stock" },
                values: new object[] { new Guid("f326544a-6546-4f3e-b65f-076827afb7b5"), "电器", "热水器", null, 300m, null, 200 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "Name", "OrderId", "Price", "ShoppingBracketId", "Stock" },
                values: new object[] { new Guid("2def1873-a4ff-4063-95c6-af9384f898c0"), "电器", "冰箱", null, 270m, null, 800 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "Name", "OrderId", "Price", "ShoppingBracketId", "Stock" },
                values: new object[] { new Guid("d760715b-07d1-4623-9cfd-8ebdba57e0b8"), "电器", "TV", null, 300m, null, 800 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("2def1873-a4ff-4063-95c6-af9384f898c0"));

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("d760715b-07d1-4623-9cfd-8ebdba57e0b8"));

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: new Guid("f326544a-6546-4f3e-b65f-076827afb7b5"));

            migrationBuilder.RenameColumn(
                name: "Account",
                table: "Customer",
                newName: "Accout");

            migrationBuilder.RenameColumn(
                name: "Account",
                table: "AdminUser",
                newName: "Accout");

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "Name", "OrderId", "Price", "ShoppingBracketId", "Stock" },
                values: new object[] { new Guid("add77178-3619-4cd5-aa54-f79ab42df1bc"), "电器", "热水器", null, 300m, null, 200 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "Name", "OrderId", "Price", "ShoppingBracketId", "Stock" },
                values: new object[] { new Guid("4284cb01-5786-4112-9ecc-8ddf0c5cd17d"), "电器", "冰箱", null, 270m, null, 800 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Class", "Name", "OrderId", "Price", "ShoppingBracketId", "Stock" },
                values: new object[] { new Guid("81b4ab69-d459-47fa-81d6-58417ee5e7eb"), "电器", "TV", null, 300m, null, 800 });
        }
    }
}
