using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeaTime.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addStoreRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Address", "City", "Description", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "台中市北區三民路三段129號", "台中市", "鄰近台中一中商圈，學生消暑勝地。", "台中一中店", "04-1234-5678" },
                    { 2, "台北市大安區大安路一段11號", "台北市", "濃厚的教育文化及熱鬧繁華的商圈，豐富整個氛圍。", "台北大安店", "02-2345-6789" },
                    { 3, "台南市安平區安平路123號", "台南市", "歷史造就了安平區的獨特風貌，茶香中蘊含了悠遠的歷史。", "台南安平店", "06-3456-7890" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
