using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Repository;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services
//__________________________________________________________________________________________

// Configure the connection string for the database
builder.Services.AddDbContext<ArtworksSharingPlaformDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServerConnection")));

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token")?.Value ?? throw new Exception("Invalid Token in configuration"))),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    option.OperationFilter<SecurityRequirementsOperationFilter>();
});

// Add Repository
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IArtworkRepository, ArtworkRepository>();
builder.Services.AddScoped<IArtworkTypeRepository, ArtworkTypeRepository>();
builder.Services.AddScoped<IBookingArtworkRepository, BookingArtworkRepository>();
builder.Services.AddScoped<IBookingArtworkTypeRepository, BookingArtworkTypeRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IComplantRepository, ComplantRepository>();
builder.Services.AddScoped<ILikeByRepository, LikeByRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaymentHistoryRepository, PaymentHistoryRepository>();
builder.Services.AddScoped<IPostArtworkRepository, PostArtworkRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPreOrderRepository, PreOrderRepository>();
builder.Services.AddScoped<IRequestArtworkRepository, RequestArtworkRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ISharingRepository, SharingRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<ITypeOfArtworkRepository, TypeOfArtworkRepository>();
builder.Services.AddScoped<IUserFollowerRepository, UserFollowerRepository>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

// Add Service

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IArtworkService, ArtworkService>();
builder.Services.AddScoped<IArtworkTypeService, ArtworkTypeService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IComplantService, ComplantService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IHelpperService, HelpperService>();
builder.Services.AddScoped<ILikeByService, LikeByService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPostArtworkService, PostArtworkService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPreOrderService, PreOrderService>();
builder.Services.AddScoped<IRequestArtworkService, RequestArtworkService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISharingService, SharingService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<ITypeOfArtworkService, TypeOfArtworkService>();
builder.Services.AddScoped<IUserFollowerService, UserFollowerService>();

// Add HtppContextAccessor
builder.Services.AddHttpContextAccessor();

//__________________________________________________________________________________________
#endregion 

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Artwork_Sharing_Plaform"));

app.MapControllers();

app.UseCors("AllowAll");

app.Run();
