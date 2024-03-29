using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Artworks_Sharing_Plaform_Api.Migrations
{
    /// <inheritdoc />
    public partial class v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfArtwork",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArtworkTypeImageDeafault = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfArtwork", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Account_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Booking_Account_CreatorId",
                        column: x => x.CreatorId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Booking_Account_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Booking_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Payment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Post",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentPost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_Account_CreatorId",
                        column: x => x.CreatorId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserFollow",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFollow_Account_FollowingId",
                        column: x => x.FollowingId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFollow_Account_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestArtwork",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestArtwork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestArtwork_Booking_BookingId",
                        column: x => x.BookingId,
                        principalSchema: "dbo",
                        principalTable: "Booking",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestArtwork_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Artwork",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artwork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artwork_Account_CreatorId",
                        column: x => x.CreatorId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Artwork_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Artwork_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sharing",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sharing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sharing_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sharing_Post_PostId",
                        column: x => x.PostId,
                        principalSchema: "dbo",
                        principalTable: "Post",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArtworkType",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeOfArtworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtworkType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtworkType_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalSchema: "dbo",
                        principalTable: "Artwork",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArtworkType_TypeOfArtwork_TypeOfArtworkId",
                        column: x => x.TypeOfArtworkId,
                        principalSchema: "dbo",
                        principalTable: "TypeOfArtwork",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookingArtwork",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestArtworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingArtwork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingArtwork_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalSchema: "dbo",
                        principalTable: "Artwork",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookingArtwork_Booking_BookingId",
                        column: x => x.BookingId,
                        principalSchema: "dbo",
                        principalTable: "Booking",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookingArtwork_RequestArtwork_RequestArtworkId",
                        column: x => x.RequestArtworkId,
                        principalSchema: "dbo",
                        principalTable: "RequestArtwork",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comment_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalSchema: "dbo",
                        principalTable: "Artwork",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comment_Post_PostId",
                        column: x => x.PostId,
                        principalSchema: "dbo",
                        principalTable: "Post",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostArtwork",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostArtwork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostArtwork_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalSchema: "dbo",
                        principalTable: "Artwork",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostArtwork_Post_PostId",
                        column: x => x.PostId,
                        principalSchema: "dbo",
                        principalTable: "Post",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookingArtworkType",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtworkType = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingArtworkType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingArtworkType_ArtworkType_ArtworkType",
                        column: x => x.ArtworkType,
                        principalSchema: "dbo",
                        principalTable: "ArtworkType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookingArtworkType_Booking_BookingId",
                        column: x => x.BookingId,
                        principalSchema: "dbo",
                        principalTable: "Booking",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Complant",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComplantContent = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AccountComplantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ArtworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SharingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComplantType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ManageIssuseAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complant_Account_AccountComplantId",
                        column: x => x.AccountComplantId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Complant_Account_ManageIssuseAccountId",
                        column: x => x.ManageIssuseAccountId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Complant_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalSchema: "dbo",
                        principalTable: "Artwork",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Complant_Comment_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "dbo",
                        principalTable: "Comment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Complant_Post_PostId",
                        column: x => x.PostId,
                        principalSchema: "dbo",
                        principalTable: "Post",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Complant_Sharing_SharingId",
                        column: x => x.SharingId,
                        principalSchema: "dbo",
                        principalTable: "Sharing",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Complant_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LikeBy",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeBy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeBy_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LikeBy_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalSchema: "dbo",
                        principalTable: "Artwork",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LikeBy_Comment_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "dbo",
                        principalTable: "Comment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LikeBy_Post_PostId",
                        column: x => x.PostId,
                        principalSchema: "dbo",
                        principalTable: "Post",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_RoleId",
                schema: "dbo",
                table: "Account",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_StatusId",
                schema: "dbo",
                table: "Account",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Artwork_CreatorId",
                schema: "dbo",
                table: "Artwork",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Artwork_OrderId",
                schema: "dbo",
                table: "Artwork",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Artwork_StatusId",
                schema: "dbo",
                table: "Artwork",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkType_ArtworkId",
                schema: "dbo",
                table: "ArtworkType",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkType_TypeOfArtworkId",
                schema: "dbo",
                table: "ArtworkType",
                column: "TypeOfArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CreatorId",
                schema: "dbo",
                table: "Booking",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_StatusId",
                schema: "dbo",
                table: "Booking",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UserId",
                schema: "dbo",
                table: "Booking",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingArtwork_ArtworkId",
                schema: "dbo",
                table: "BookingArtwork",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingArtwork_BookingId",
                schema: "dbo",
                table: "BookingArtwork",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingArtwork_RequestArtworkId",
                schema: "dbo",
                table: "BookingArtwork",
                column: "RequestArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingArtworkType_ArtworkType",
                schema: "dbo",
                table: "BookingArtworkType",
                column: "ArtworkType");

            migrationBuilder.CreateIndex(
                name: "IX_BookingArtworkType_BookingId",
                schema: "dbo",
                table: "BookingArtworkType",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AccountId",
                schema: "dbo",
                table: "Comment",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ArtworkId",
                schema: "dbo",
                table: "Comment",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                schema: "dbo",
                table: "Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Complant_AccountComplantId",
                schema: "dbo",
                table: "Complant",
                column: "AccountComplantId");

            migrationBuilder.CreateIndex(
                name: "IX_Complant_ArtworkId",
                schema: "dbo",
                table: "Complant",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Complant_CommentId",
                schema: "dbo",
                table: "Complant",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Complant_ManageIssuseAccountId",
                schema: "dbo",
                table: "Complant",
                column: "ManageIssuseAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Complant_PostId",
                schema: "dbo",
                table: "Complant",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Complant_SharingId",
                schema: "dbo",
                table: "Complant",
                column: "SharingId");

            migrationBuilder.CreateIndex(
                name: "IX_Complant_StatusId",
                schema: "dbo",
                table: "Complant",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeBy_AccountId",
                schema: "dbo",
                table: "LikeBy",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeBy_ArtworkId",
                schema: "dbo",
                table: "LikeBy",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeBy_CommentId",
                schema: "dbo",
                table: "LikeBy",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeBy_PostId",
                schema: "dbo",
                table: "LikeBy",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_AccountId",
                schema: "dbo",
                table: "Order",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StatusId",
                schema: "dbo",
                table: "Order",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CreatorId",
                schema: "dbo",
                table: "Post",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PostArtwork_ArtworkId",
                schema: "dbo",
                table: "PostArtwork",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_PostArtwork_PostId",
                schema: "dbo",
                table: "PostArtwork",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestArtwork_BookingId",
                schema: "dbo",
                table: "RequestArtwork",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestArtwork_StatusId",
                schema: "dbo",
                table: "RequestArtwork",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Sharing_AccountId",
                schema: "dbo",
                table: "Sharing",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Sharing_PostId",
                schema: "dbo",
                table: "Sharing",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollow_FollowingId",
                schema: "dbo",
                table: "UserFollow",
                column: "FollowingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollow_UserId",
                schema: "dbo",
                table: "UserFollow",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingArtwork",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "BookingArtworkType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Complant",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LikeBy",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PostArtwork",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserFollow",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RequestArtwork",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ArtworkType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sharing",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Comment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Booking",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TypeOfArtwork",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Artwork",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Post",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Status",
                schema: "dbo");
        }
    }
}
