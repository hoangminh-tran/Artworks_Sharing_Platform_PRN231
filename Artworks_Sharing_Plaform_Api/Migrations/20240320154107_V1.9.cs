using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Artworks_Sharing_Plaform_Api.Migrations
{
    /// <inheritdoc />
    public partial class V19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                schema: "dbo",
                table: "TypeOfArtwork",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfArtwork_AccountId",
                schema: "dbo",
                table: "TypeOfArtwork",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeOfArtwork_Account_AccountId",
                schema: "dbo",
                table: "TypeOfArtwork",
                column: "AccountId",
                principalSchema: "dbo",
                principalTable: "Account",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeOfArtwork_Account_AccountId",
                schema: "dbo",
                table: "TypeOfArtwork");

            migrationBuilder.DropIndex(
                name: "IX_TypeOfArtwork_AccountId",
                schema: "dbo",
                table: "TypeOfArtwork");

            migrationBuilder.DropColumn(
                name: "AccountId",
                schema: "dbo",
                table: "TypeOfArtwork");
        }
    }
}
