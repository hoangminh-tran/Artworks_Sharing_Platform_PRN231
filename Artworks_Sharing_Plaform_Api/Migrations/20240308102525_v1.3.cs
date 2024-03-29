using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Artworks_Sharing_Plaform_Api.Migrations
{
    /// <inheritdoc />
    public partial class v13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                schema: "dbo",
                table: "TypeOfArtwork",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfArtwork_StatusId",
                schema: "dbo",
                table: "TypeOfArtwork",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeOfArtwork_Status_StatusId",
                schema: "dbo",
                table: "TypeOfArtwork",
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
                name: "FK_TypeOfArtwork_Status_StatusId",
                schema: "dbo",
                table: "TypeOfArtwork");

            migrationBuilder.DropIndex(
                name: "IX_TypeOfArtwork_StatusId",
                schema: "dbo",
                table: "TypeOfArtwork");

            migrationBuilder.DropColumn(
                name: "StatusId",
                schema: "dbo",
                table: "TypeOfArtwork");
        }
    }
}
