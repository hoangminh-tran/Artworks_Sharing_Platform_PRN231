using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Artworks_Sharing_Plaform_Api.Migrations
{
    /// <inheritdoc />
    public partial class v18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtpCode",
                schema: "dbo",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpCodeCreated",
                schema: "dbo",
                table: "Account",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpCodeExpired",
                schema: "dbo",
                table: "Account",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtpCode",
                schema: "dbo",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "OtpCodeCreated",
                schema: "dbo",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "OtpCodeExpired",
                schema: "dbo",
                table: "Account");
        }
    }
}
