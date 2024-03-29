using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Artworks_Sharing_Plaform_Api.Migrations
{
    /// <inheritdoc />
    public partial class v14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingArtworkType_ArtworkType_ArtworkType",
                schema: "dbo",
                table: "BookingArtworkType");

            migrationBuilder.RenameColumn(
                name: "ArtworkType",
                schema: "dbo",
                table: "BookingArtworkType",
                newName: "TypeOfArtworkId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingArtworkType_ArtworkType",
                schema: "dbo",
                table: "BookingArtworkType",
                newName: "IX_BookingArtworkType_TypeOfArtworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingArtworkType_TypeOfArtwork_TypeOfArtworkId",
                schema: "dbo",
                table: "BookingArtworkType",
                column: "TypeOfArtworkId",
                principalSchema: "dbo",
                principalTable: "TypeOfArtwork",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingArtworkType_TypeOfArtwork_TypeOfArtworkId",
                schema: "dbo",
                table: "BookingArtworkType");

            migrationBuilder.RenameColumn(
                name: "TypeOfArtworkId",
                schema: "dbo",
                table: "BookingArtworkType",
                newName: "ArtworkType");

            migrationBuilder.RenameIndex(
                name: "IX_BookingArtworkType_TypeOfArtworkId",
                schema: "dbo",
                table: "BookingArtworkType",
                newName: "IX_BookingArtworkType_ArtworkType");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingArtworkType_ArtworkType_ArtworkType",
                schema: "dbo",
                table: "BookingArtworkType",
                column: "ArtworkType",
                principalSchema: "dbo",
                principalTable: "ArtworkType",
                principalColumn: "Id");
        }
    }
}
