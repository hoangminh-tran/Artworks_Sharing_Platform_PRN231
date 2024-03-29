using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Artworks_Sharing_Plaform_Api.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreOrder_Account_AccountId",
                table: "PreOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PreOrder_Artwork_ArtworkId",
                table: "PreOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PreOrder_Status_StatusId",
                table: "PreOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreOrder",
                table: "PreOrder");

            migrationBuilder.RenameTable(
                name: "PreOrder",
                newName: "PreOrders");

            migrationBuilder.RenameIndex(
                name: "IX_PreOrder_StatusId",
                table: "PreOrders",
                newName: "IX_PreOrders_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_PreOrder_ArtworkId",
                table: "PreOrders",
                newName: "IX_PreOrders_ArtworkId");

            migrationBuilder.RenameIndex(
                name: "IX_PreOrder_AccountId",
                table: "PreOrders",
                newName: "IX_PreOrders_AccountId");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                schema: "dbo",
                table: "Account",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreOrders",
                table: "PreOrders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PaymentHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentHistories_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentHistories_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistories_AccountId",
                table: "PaymentHistories",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistories_StatusId",
                table: "PaymentHistories",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreOrders_Account_AccountId",
                table: "PreOrders",
                column: "AccountId",
                principalSchema: "dbo",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreOrders_Artwork_ArtworkId",
                table: "PreOrders",
                column: "ArtworkId",
                principalSchema: "dbo",
                principalTable: "Artwork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreOrders_Status_StatusId",
                table: "PreOrders",
                column: "StatusId",
                principalSchema: "dbo",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreOrders_Account_AccountId",
                table: "PreOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PreOrders_Artwork_ArtworkId",
                table: "PreOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PreOrders_Status_StatusId",
                table: "PreOrders");

            migrationBuilder.DropTable(
                name: "PaymentHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreOrders",
                table: "PreOrders");

            migrationBuilder.DropColumn(
                name: "Balance",
                schema: "dbo",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "PreOrders",
                newName: "PreOrder");

            migrationBuilder.RenameIndex(
                name: "IX_PreOrders_StatusId",
                table: "PreOrder",
                newName: "IX_PreOrder_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_PreOrders_ArtworkId",
                table: "PreOrder",
                newName: "IX_PreOrder_ArtworkId");

            migrationBuilder.RenameIndex(
                name: "IX_PreOrders_AccountId",
                table: "PreOrder",
                newName: "IX_PreOrder_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreOrder",
                table: "PreOrder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreOrder_Account_AccountId",
                table: "PreOrder",
                column: "AccountId",
                principalSchema: "dbo",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreOrder_Artwork_ArtworkId",
                table: "PreOrder",
                column: "ArtworkId",
                principalSchema: "dbo",
                principalTable: "Artwork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreOrder_Status_StatusId",
                table: "PreOrder",
                column: "StatusId",
                principalSchema: "dbo",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
