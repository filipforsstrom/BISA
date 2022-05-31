using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BISA.Server.Migrations
{
    public partial class addedItemInItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ItemInventory",
                columns: new[] { "Id", "Available", "ItemId" },
                values: new object[,]
                {
                    { 19, true, 10 },
                    { 20, true, 10 }
                });

            migrationBuilder.UpdateData(
                table: "LoanReservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5400), new DateTime(2022, 6, 20, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5402) });

            migrationBuilder.UpdateData(
                table: "LoanReservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5405), new DateTime(2022, 6, 20, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5408) });

            migrationBuilder.UpdateData(
                table: "LoanReservations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5410), new DateTime(2022, 6, 7, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5413) });

            migrationBuilder.UpdateData(
                table: "LoanReservations",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5415), new DateTime(2022, 6, 27, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5417) });

            migrationBuilder.UpdateData(
                table: "LoansActive",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5280), new DateTime(2022, 6, 20, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5318) });

            migrationBuilder.UpdateData(
                table: "LoansActive",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5322), new DateTime(2022, 6, 20, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5324) });

            migrationBuilder.UpdateData(
                table: "LoansActive",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5327), new DateTime(2022, 6, 7, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5329) });

            migrationBuilder.UpdateData(
                table: "LoansActive",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5333), new DateTime(2022, 6, 20, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5335) });

            migrationBuilder.UpdateData(
                table: "LoansActive",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5337), new DateTime(2022, 6, 7, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5340) });

            migrationBuilder.UpdateData(
                table: "LoansActive",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5342), new DateTime(2022, 6, 7, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5344) });

            migrationBuilder.UpdateData(
                table: "LoansActive",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5347), new DateTime(2022, 6, 20, 14, 43, 15, 797, DateTimeKind.Local).AddTicks(5349) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemInventory",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ItemInventory",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.UpdateData(
                table: "LoanReservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2870), new DateTime(2022, 6, 20, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2873) });

            migrationBuilder.UpdateData(
                table: "LoanReservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2877), new DateTime(2022, 6, 20, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2879) });

            migrationBuilder.UpdateData(
                table: "LoanReservations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2882), new DateTime(2022, 6, 7, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2884) });

            migrationBuilder.UpdateData(
                table: "LoanReservations",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 6, 20, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2887), new DateTime(2022, 6, 27, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2889) });

            migrationBuilder.UpdateData(
                table: "LoansActive",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2710), new DateTime(2022, 6, 20, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "LoansActive",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2755), new DateTime(2022, 6, 20, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2757) });

            migrationBuilder.UpdateData(
                table: "LoansActive",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2760), new DateTime(2022, 6, 7, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2762) });

            migrationBuilder.UpdateData(
                table: "LoansActive",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2765), new DateTime(2022, 6, 20, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2767) });

            migrationBuilder.UpdateData(
                table: "LoansActive",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2770), new DateTime(2022, 6, 7, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2772) });

            migrationBuilder.UpdateData(
                table: "LoansActive",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2775), new DateTime(2022, 6, 7, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2777) });

            migrationBuilder.UpdateData(
                table: "LoansActive",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Date_From", "Date_To" },
                values: new object[] { new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2780), new DateTime(2022, 6, 20, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2782) });
        }
    }
}
