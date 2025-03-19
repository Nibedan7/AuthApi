using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class seedDataIntoEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Designation", "Joining_date", "Name" },
                values: new object[,]
                {
                    { 1, "IT", "Software Engineer", new DateOnly(2020, 5, 15), "John Doe" },
                    { 2, "HR", "HR Manager", new DateOnly(2018, 3, 22), "Jane Smith" },
                    { 3, "Marketing", "Marketing Specialist", new DateOnly(2021, 8, 10), "Alice Brown" },
                    { 4, "Product", "Product Manager", new DateOnly(2017, 11, 1), "Bob Johnson" },
                    { 5, "IT", "QA Engineer", new DateOnly(2019, 6, 30), "Charlie Davis" },
                    { 6, "Business", "Business Analyst", new DateOnly(2020, 2, 5), "David Wilson" },
                    { 7, "Sales", "Sales Executive", new DateOnly(2022, 7, 20), "Emily Clark" },
                    { 8, "IT", "Systems Architect", new DateOnly(2016, 4, 18), "Frank Harris" },
                    { 9, "Support", "Customer Support", new DateOnly(2021, 1, 12), "Grace Lewis" },
                    { 10, "Design", "UI/UX Designer", new DateOnly(2019, 9, 25), "Henry Walker" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
