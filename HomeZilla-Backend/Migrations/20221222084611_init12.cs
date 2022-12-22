using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeZillaBackend.Migrations
{
    /// <inheritdoc />
    public partial class init12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProviderProviderServices");

            migrationBuilder.AddColumn<Guid>(
                name: "ProviderId",
                table: "ProviderServices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProviderServices_ProviderId",
                table: "ProviderServices",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderServices_Provider_ProviderId",
                table: "ProviderServices",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderServices_Provider_ProviderId",
                table: "ProviderServices");

            migrationBuilder.DropIndex(
                name: "IX_ProviderServices_ProviderId",
                table: "ProviderServices");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "ProviderServices");

            migrationBuilder.CreateTable(
                name: "ProviderProviderServices",
                columns: table => new
                {
                    ProviderIdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceIdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderProviderServices", x => new { x.ProviderIdId, x.ServiceIdId });
                    table.ForeignKey(
                        name: "FK_ProviderProviderServices_ProviderServices_ServiceIdId",
                        column: x => x.ServiceIdId,
                        principalTable: "ProviderServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProviderProviderServices_Provider_ProviderIdId",
                        column: x => x.ProviderIdId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProviderProviderServices_ServiceIdId",
                table: "ProviderProviderServices",
                column: "ServiceIdId");
        }
    }
}
