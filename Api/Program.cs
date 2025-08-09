using Application.Interfaces;
using Application.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


#region ConfigJWT

var jwtSetting = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSetting.GetValue<string>("Secret");

builder.Services.AddAuthentication(Options =>
{
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = jwtSetting.GetValue<string>("Issuer"),
    ValidAudience = jwtSetting.GetValue<string>("Audience"),
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
    ClockSkew = TimeSpan.Zero
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("requireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("requireGuestRole", policy => policy.RequireRole("Guest"));
    options.AddPolicy("requireCustomerRole", policy => policy.RequireRole("Customer"));
});
#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

//service
builder.Services.AddScoped<LoginUserService>(provider => new LoginUserService(
    provider.GetRequiredService<IUserRepository>(), secretKey
    ));

builder.Services.AddScoped<RegisterUserService>();

//Context
builder.Services.AddDbContext<AppCoreContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("defaultContext")));





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

app.Run();
