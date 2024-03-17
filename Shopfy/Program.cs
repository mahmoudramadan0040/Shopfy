using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using ServiceStack.Text;
using Shopfy.Models;
using Shopfy.Models.Interfaces;
using Shopfy.Models.Repository;
using System.Text;
using Shopfy.Controllers;

//-------------------------------------------------------//
//       Create Serilog configuration                   //
//-----------------------------------------------------//

Log.Logger = new LoggerConfiguration()
       .MinimumLevel.Debug()
       .WriteTo.Console()
       .WriteTo.File("Logs/log.txt",rollingInterval:RollingInterval.Day)
       .CreateLogger();
//=======================================================//

       
var builder = WebApplication.CreateBuilder(args);

//-------------------------------------------------------//
//       Serilog configuration                          //
//-----------------------------------------------------//
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Host.UseSerilog();
// end config serilog 



// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShopfyDbContext>(options => {
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:ShopfyDbContextConnection"]);
});


//-------------------------------------------------------//
//        Add Identity  configuration                   //
//-----------------------------------------------------//
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(o =>
{
    o.Password.RequireDigit = true;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequiredLength = 4;
    o.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ShopfyDbContext>()
.AddDefaultTokenProviders();

//-------------------------------------------------------//
//              Adding Authentication                   //
//-----------------------------------------------------//
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
#pragma warning disable CS8604 // Possible null reference argument.
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,

            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
#pragma warning restore CS8604 // Possible null reference argument.
    });
//-------------------------------------------------------//
//              ---------------------                   //
//-----------------------------------------------------//



//-------------------------------------------------------//
//              activate service repos                  //
//-----------------------------------------------------//

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
/*builder.Services.AddSingleton<IStorageRepository, S3StorageRepository>();*/
builder.Services.AddSingleton<IStorageRepository, LocalStorageRepository>();

//-------------------------------------------------------//
//              add mapper configration                 //
//-----------------------------------------------------//
builder.Services.AddAutoMapper(typeof(Program));
//-------------------------------------------------------//
//              add AWS configration                    //
//-----------------------------------------------------//
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
    Path.Combine(builder.Environment.ContentRootPath, "Resources")),
    RequestPath = "/resources"
});
app.UseHttpsRedirection();
// Authentication & Authorization
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();



app.Run();
