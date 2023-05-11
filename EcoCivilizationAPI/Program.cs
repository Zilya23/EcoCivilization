using EcoCivilizationAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EcoCivilizationAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
    options.AddPolicy("MyAllow", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<EcoCivilizationContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));

builder.Services.AddAuthorization();

builder.Services.AddSingleton<TokenService>();

Console.WriteLine("Debug " + builder.Configuration.GetConnectionString("connection"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyAllow");

app.UseAuthorization();

app.MapControllers();

//app.Map("/login/{username}", (string username) =>
//{
//    var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
//    // создаем JWT-токен
//    var jwt = new JwtSecurityToken(
//            issuer: AuthOptions.ISSUER,
//            audience: AuthOptions.AUDIENCE,
//            claims: claims,
//            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
//            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

//    return new JwtSecurityTokenHandler().WriteToken(jwt);
//});

app.Map("/data", [Authorize] () => new { message = "Hello World!" });

app.Run();

//public class AuthOptions
//{
//    public const string ISSUER = "MyAuthServer"; // издатель токена
//    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
//    const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
//    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
//        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
//}
