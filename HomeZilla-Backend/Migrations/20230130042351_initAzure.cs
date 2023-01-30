using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeZillaBackend.Migrations
{
    /// <inheritdoc />
    public partial class initAzure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "OrderDetails",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
