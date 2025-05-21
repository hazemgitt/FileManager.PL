using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileManager.PL.Migrations
{
    /// <inheritdoc />
    public partial class seeding_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "Description" },
                values: new object[,]
                {

                    { 2, "London", "Capital of England" },
                    { 3, "Tokyo", "Capital of Japan" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Name", "Address", "CityId" },
                values: new object[,]
                {
                    { 1, "Manhattan Office", "123 Broadway", 1 },
                    { 2, "Brooklyn Warehouse", "456 Atlantic Ave", 1 },
                    { 3, "Westminster Branch", "10 Downing St", 2 },
                    { 4, "Shibuya Center", "1-1 Shibuya", 3 }
                });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "Name", "Description", "LocationId" },
                values: new object[,]
                {
                    { 1, "Inspection Reports", "Annual building inspections", 1 },
                    { 2, "Maintenance Logs", "Regular maintenance records", 1 },
                    { 3, "Inventory Lists", "Current inventory", 2 },
                    { 4, "Safety Documents", "Safety procedures and certificates", 3 }
        });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
