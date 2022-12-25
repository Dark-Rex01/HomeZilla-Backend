using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeZillaBackend.Migrations
{
    /// <inheritdoc />
    public partial class init01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Customer_customerId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Provider_providerId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "providerId",
                table: "OrderDetails",
                newName: "ProviderId");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "OrderDetails",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_providerId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_customerId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_CustomerId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProviderId",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Customer_CustomerId",
                table: "OrderDetails",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Provider_ProviderId",
                table: "OrderDetails",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Customer_CustomerId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Provider_ProviderId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "OrderDetails",
                newName: "providerId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "OrderDetails",
                newName: "customerId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProviderId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_providerId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_CustomerId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_customerId");

            migrationBuilder.AlterColumn<Guid>(
                name: "providerId",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "customerId",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Customer_customerId",
                table: "OrderDetails",
                column: "customerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Provider_providerId",
                table: "OrderDetails",
                column: "providerId",
                principalTable: "Provider",
                principalColumn: "Id");
        }
    }
}
