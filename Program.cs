using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Task_Manager.Configurations.Utils;
using Task_Manager.Core;
using Task_Manager.Core.Interfaces;
using Task_Manager.Data;
using System.Text;
using Task_Manager.Configurations.Jwt;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Add Db Context
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value!));
    jwt.SaveToken = true;

    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false, // Only for dev
        ValidateAudience = false, // Only for dev
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        RequireExpirationTime = false,
        ValidateLifetime = true,
    };
});

builder.Services.AddIdentityCore<IdentityUser>(o => o.SignIn.RequireConfirmedEmail = false).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>();

// builder.Services.AddIdentityCore<IdentityRole>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddAuthentication("Bearer").AddJwtBearer(o =>
// {

//     string keyInput = "4567890-kadsfjadks!@#$%^&*()_SDFGHJKL::LKJHXCVBNM)(*&^%$_)(*&^+_)(*!@#$%^&*())";
//     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyInput));


//     o.RequireHttpsMetadata = true;
//     o.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         IssuerSigningKey = key,
//         ValidateLifetime = true,
//         ValidIssuer = "MY_APP",

//         ValidAudience = "FRONT_END"
//     };
// });

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

app.MapControllers();
app.AddGlobalErrorHandler();

app.Run();
