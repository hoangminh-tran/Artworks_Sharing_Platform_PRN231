using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Artworks_Sharing_Plaform_Api.AppDatabaseContext
{
    public class ArtworksSharingPlaformDatabaseContext : DbContext
    {
        public ArtworksSharingPlaformDatabaseContext(DbContextOptions<ArtworksSharingPlaformDatabaseContext> options) : base(options)
        {
            // When initialize database, call Initialize method
            //Initialize();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.CreatorArtwork)
                .WithOne(e => e.Creator)
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.UserBooking)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.CreatorBooking)
                .WithOne(e => e.Creator)
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.AccountComment)
                .WithOne(e => e.Account)
                .HasForeignKey(e => e.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.AccountComplant)
                .WithOne(e => e.AccountComplant)
                .HasForeignKey(e => e.AccountComplantId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.ManageIssuseAccount)
                .WithOne(e => e.ManageIssuseAccount)
                .HasForeignKey(e => e.ManageIssuseAccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.AccountLikeBy)
                .WithOne(e => e.Account)
                .HasForeignKey(e => e.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.AccountOrder)
                .WithOne(e => e.Account)
                .HasForeignKey(e => e.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.CreatorPost)
                .WithOne(e => e.Creator)
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.UserSharing)
                .WithOne(e => e.Account)
                .HasForeignKey(e => e.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.UserFollow)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.UserFollowing)
                .WithOne(e => e.Following)
                .HasForeignKey(e => e.FollowingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Artwork>()
                .HasMany(e => e.ArtworkType)
                .WithOne(e => e.Artwork)
                .HasForeignKey(e => e.ArtworkId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Artwork>()
                .HasMany(e => e.ArtworkBooking)
                .WithOne(e => e.Artwork)
                .HasForeignKey(e => e.ArtworkId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Artwork>()
                .HasMany(e => e.Comment)
                .WithOne(e => e.Artwork)
                .HasForeignKey(e => e.ArtworkId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Artwork>()
                .HasMany(e => e.Complant)
                .WithOne(e => e.Artwork)
                .HasForeignKey(e => e.ArtworkId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Artwork>()
                .HasMany(e => e.LikeBy)
                .WithOne(e => e.Artwork)
                .HasForeignKey(e => e.ArtworkId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Artwork>()
                .HasMany(e => e.PostArtworks)
                .WithOne(e => e.Artwork)
                .HasForeignKey(e => e.ArtworkId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasMany(e => e.BookingArtworkTypes)
                .WithOne(e => e.Booking)
                .HasForeignKey(e => e.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasMany(e => e.BookingArtworks)
                .WithOne(e => e.Booking)
                .HasForeignKey(e => e.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasMany(e => e.RequestArtworks)
                .WithOne(e => e.Booking)
                .HasForeignKey(e => e.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasMany(e => e.Complants)
                .WithOne(e => e.Comment)
                .HasForeignKey(e => e.CommentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasMany(e => e.LikeBys)
                .WithOne(e => e.Comment)
                .HasForeignKey(e => e.CommentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Artworks)
                .WithOne(e => e.Order)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Complants)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.LikeBys)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.PostArtworks)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Sharings)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RequestArtwork>()
                .HasMany(e => e.BookingArtworks)
                .WithOne(e => e.RequestArtwork)
                .HasForeignKey(e => e.RequestArtworkId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Accounts)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sharing>()
                .HasMany(e => e.Complants)
                .WithOne(e => e.Sharing)
                .HasForeignKey(e => e.SharingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Accounts)
                .WithOne(e => e.Status)
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Artworks)
                .WithOne(e => e.Status)
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Bookings)
                .WithOne(e => e.Status)
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Complants)
                .WithOne(e => e.Status)
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Orders)
                .WithOne(e => e.Status)
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.RequestArtworks)
                .WithOne(e => e.Status)
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TypeOfArtwork>()
                .HasMany(e => e.ArtworkTypes)
                .WithOne(e => e.TypeOfArtwork)
                .HasForeignKey(e => e.TypeOfArtworkId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TypeOfArtwork>()
                .HasMany(e => e.BookingArtworkTypes)
                .WithOne(e => e.TypeOfArtwork)
                .HasForeignKey(e => e.TypeOfArtworkId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Artwork>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,0)");

            modelBuilder.Entity<Account>()
                .Property(e => e.Balance)
                .HasColumnType("decimal(18,0)");

            modelBuilder.Entity<PaymentHistory>()
                .Property(e => e.Amount)
                .HasColumnType("decimal(18,0)");

            modelBuilder.Entity<Booking>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,0)");
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<ArtworkType> ArtworkTypes { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingArtwork> BookingArtworks { get; set; }
        public DbSet<BookingArtworkType> BookingArtworkTypes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Complant> Complants { get; set; }
        public DbSet<LikeBy> LikeBys { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostArtwork> PostArtworks { get; set; }
        public DbSet<RequestArtwork> RequestArtworks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sharing> Sharings { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<TypeOfArtwork> TypeOfArtworks { get; set; }
        public DbSet<UserFollow> UserFollowers { get; set; }
        public DbSet<PreOrder> PreOrders { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }

        public void Initialize()
        {
            if (!Roles.Any())
            {
                Roles.Add(new Role
                {
                    RoleName = RoleEnum.ADMIN,
                    CreateDateTime = DateTime.Now
                });

                Roles.Add(new Role
                {
                    RoleName = RoleEnum.CREATOR,
                    CreateDateTime = DateTime.Now
                });

                Roles.Add(new Role
                {
                    RoleName = RoleEnum.MEMBER,
                    CreateDateTime = DateTime.Now
                });

                Roles.Add(new Role
                {
                    RoleName = RoleEnum.MODERATOR,
                    CreateDateTime = DateTime.Now
                });

                SaveChanges();
            }

            if (!Statuses.Any())
            {
                Statuses.Add(new Status
                {
                    StatusName = StatusEnum.ACTIVE,
                    CreateDateTime = DateTime.Now
                });

                Statuses.Add(new Status
                {
                    StatusName = AccountStatusEnum.DEACTIVE,
                    CreateDateTime = DateTime.Now
                });

                Statuses.Add(new Status
                {
                    StatusName = ArtworkStatusEnum.PRIVATE,
                    CreateDateTime = DateTime.Now
                });

                Statuses.Add(new Status
                {
                    StatusName = ArtworkStatusEnum.PUBLIC,
                    CreateDateTime = DateTime.Now
                });

                Statuses.Add(new Status
                {
                    StatusName = RequestArtworkStatusEnum.PENDING,
                    CreateDateTime = DateTime.Now
                });

                Statuses.Add(new Status
                {
                    StatusName = RequestArtworkStatusEnum.ACCEPTED,
                    CreateDateTime = DateTime.Now
                });

                Statuses.Add(new Status
                {
                    StatusName = RequestArtworkStatusEnum.REJECTED,
                    CreateDateTime = DateTime.Now
                });

                Statuses.Add(new Status
                {
                    StatusName = RequestArtworkStatusEnum.CANCEL,
                    CreateDateTime = DateTime.Now
                });

                Statuses.Add(new Status
                {
                    StatusName = OrderStatusEnum.PAID,
                    CreateDateTime = DateTime.Now
                });

                SaveChanges();
            }

            if (!Accounts.Any())
            {
                // Code to initialize Account data
                var passHash = CreatePassHashAndPassSalt("Admin@123", out byte[] passwordSalt);
                var passHashMember = CreatePassHashAndPassSalt("Member@123", out byte[] passwordSaltMember);
                var passHashModerator = CreatePassHashAndPassSalt("Moderator@123", out byte[] passwordSaltModerator);
                var passHashCreator = CreatePassHashAndPassSalt("Creator@123", out byte[] passwordSaltCreator);
                var roleId = Roles.FirstOrDefault(x => x.RoleName == "ADMIN");
                var roleMemberId = Roles.FirstOrDefault(x => x.RoleName.Equals(RoleEnum.MEMBER)) ?? throw new Exception("Can not find role");
                var roleModeratorId = Roles.FirstOrDefault(x => x.RoleName == RoleEnum.MODERATOR) ?? throw new Exception("Can not find role");
                var roleCreatorId = Roles.FirstOrDefault(x => x.RoleName == RoleEnum.CREATOR) ?? throw new Exception("Can not find role");
                var statusId = Statuses.FirstOrDefault(x => x.StatusName.Equals(StatusEnum.ACTIVE));
                if (roleId == null || statusId == null)
                {
                    throw new Exception("Can not find role or status");
                }
                Accounts.Add(new Account
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@localhost.com",
                    PasswordHash = passHash,
                    PasswordSalt = Convert.ToBase64String(passwordSalt),
                    StatusId = statusId.Id,
                    RoleId = roleId.Id,
                    CreateDateTime = DateTime.Now
                });
                Accounts.Add(new Account
                {
                    FirstName = "Member",
                    LastName = "Member",
                    Email = "member@localhost.com",
                    PasswordHash = passHashMember,
                    PasswordSalt = Convert.ToBase64String(passwordSaltMember),
                    StatusId = statusId.Id,
                    RoleId = roleMemberId.Id,
                    CreateDateTime = DateTime.Now
                });
                Accounts.Add(new Account
                {
                    FirstName = "Moderator",
                    LastName = "Moderator",
                    Email = "moderator@localhost.com",
                    PasswordHash = passHashModerator,
                    PasswordSalt = Convert.ToBase64String(passwordSaltModerator),
                    StatusId = statusId.Id,
                    RoleId = roleModeratorId.Id,
                    CreateDateTime = DateTime.Now
                });

                Accounts.Add(new Account
                {
                    FirstName = "Creator",
                    LastName = "Creator",
                    Email = "creator@localhost.com",
                    PasswordHash = passHashCreator,
                    PasswordSalt = Convert.ToBase64String(passwordSaltCreator),
                    StatusId = statusId.Id,
                    RoleId = roleCreatorId.Id,
                    CreateDateTime = DateTime.Now
                });
                SaveChanges();
            }
            if (!TypeOfArtworks.Any())
            {
                var statusActiveId = Statuses.FirstOrDefault(x => x.StatusName.Equals(StatusEnum.ACTIVE)) ?? throw new Exception("Can not find status");
                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Chân dung",
                    TypeDescription = "Một bức tranh hoặc bức ảnh mô tả khuôn mặt của một người",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Phong cảnh",
                    TypeDescription = "Một bức tranh hoặc bức ảnh mô tả một cảnh quan thiên nhiên, chẳng hạn như núi, rừng hoặc sông",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Tĩnh vật",
                    TypeDescription = "Một bức tranh hoặc bức ảnh mô tả một sắp xếp các vật thể vô tri vô giác, chẳng hạn như trái cây, hoa hoặc bình",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Tranh trừu tượng",
                    TypeDescription = "Một bức tranh hoặc bức ảnh không mô tả các vật thể hoặc cảnh thực tế",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Tranh minh họa",
                    TypeDescription = "Một bức tranh hoặc bức ảnh được tạo ra để minh họa một câu chuyện hoặc bài thơ",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Sơn mài",
                    TypeDescription = "Một loại tranh sử dụng sơn mài, một loại nhựa cứng, để tạo ra một lớp hoàn thiện bóng, bền",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Aquarel",
                    TypeDescription = "Một loại tranh sử dụng màu nước, là một loại sơn được làm từ bột màu hòa tan trong nước",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Phác thảo",
                    TypeDescription = "Một bức tranh hoặc bức vẽ được tạo ra bằng cách sử dụng các đường nét đơn giản để mô tả hình dạng của một vật thể hoặc cảnh",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Chủ nghĩa biểu hiện",
                    TypeDescription = "Một phong cách nghệ thuật sử dụng màu sắc và hình dạng phóng đại để thể hiện cảm xúc của nghệ sĩ",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Lập thể",
                    TypeDescription = "Một phong cách nghệ thuật mô tả các đối tượng dưới dạng các hình dạng hình học",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Hậu ấn tượng",
                    TypeDescription = "Một phong cách nghệ thuật sử dụng màu sắc đậm và các đường viền đậm để tạo ra các tác phẩm nghệ thuật có tính trang trí cao",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Phục hưng",
                    TypeDescription = "Một phong cách nghệ thuật bắt đầu ở Ý vào thế kỷ 14 và được đặc trưng bởi sự quan tâm đến chủ nghĩa hiện thực và chủ nghĩa nhân văn",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Baroque",
                    TypeDescription = "Một phong cách nghệ thuật bắt đầu ở Ý vào thế kỷ 17 và được đặc trưng bởi việc sử dụng màu sắc, ánh sáng và chuyển động ấn tượng",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Chủ nghĩa lãng mạn",
                    TypeDescription = "Một phong cách nghệ thuật bắt đầu vào thế kỷ 18 và được đặc trưng bởi sự tập trung vào cảm xúc, trí tưởng tượng và thiên nhiên",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Chủ nghĩa hiện thực",
                    TypeDescription = "Một phong cách nghệ thuật bắt đầu vào thế kỷ 19 và được đặc trưng bởi việc mô tả các đối tượng và cảnh một cách thực tế",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Hậu ấn tượng",
                    TypeDescription = "Một phong cách nghệ thuật bắt đầu vào cuối thế kỷ 19 và được đặc trưng bởi việc sử dụng màu sắc và hình dạng mạnh mẽ để thể hiện cảm xúc của nghệ sĩ",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Chủ nghĩa biểu hiện",
                    TypeDescription = "Một phong cách nghệ thuật bắt đầu vào đầu thế kỷ 20 và được đặc trưng bởi việc sử dụng màu sắc và hình dạng bị bóp méo để thể hiện cảm xúc của nghệ sĩ",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Lập thể",
                    TypeDescription = "Một phong cách nghệ thuật bắt đầu vào đầu thế kỷ 20 và được đặc trưng bởi việc mô tả các đối tượng dưới dạng các hình dạng hình học",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Chủ nghĩa siêu thực",
                    TypeDescription = "Một phong cách nghệ thuật bắt đầu vào những năm 1920 và được đặc trưng bởi việc sử dụng hình ảnh vô thức để tạo ra các tác phẩm nghệ thuật gợi mơ và kỳ ảo",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Pop Art",
                    TypeDescription = "Một phong cách nghệ thuật bắt đầu vào những năm 1950 và được đặc trưng bởi việc sử dụng hình ảnh và biểu tượng văn hóa đại chúng",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Op Art",
                    TypeDescription = "Một phong cách nghệ thuật bắt đầu vào những năm 1960 và được đặc trưng bởi việc sử dụng các hình ảnh ảo ảnh quang học",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });

                TypeOfArtworks.Add(new TypeOfArtwork
                {
                    Type = "Nấu ăn",
                    TypeDescription = "Một phong cách nghệ thuật thể hiện các món ăn và các đối tượng liên quan đến ẩm thực",
                    StatusId = statusActiveId.Id,
                    CreateDateTime = DateTime.Now
                });
                SaveChanges();
            }
        }

        #region Private Methods
        private static string CreatePassHashAndPassSalt(string pass, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            return System.Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass)));
        }
        #endregion
    }
}
