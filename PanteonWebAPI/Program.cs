using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PanteonWebAPI.Interfaces;
using PanteonWebAPI.Mappers;
using PanteonWebAPI.Models.Data;
using PanteonWebAPI.Models.JwtModel;
using PanteonWebAPI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
}
);


builder.Services.Configure<JwtConfigs>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddScoped<BuildingConfigMapper>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql("server=localhost;port=3306;database=panteon;", ServerVersion.AutoDetect("server=localhost;port=3306;database=panteon;")));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]));


builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IBuildingType, BuildingTypeService>();
builder.Services.AddScoped<IBuildingConfiguration, BuildingConfigurationService>();

builder.Services.AddScoped<AppDbContext>();


// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000") // React uygulamanýzýn çalýþtýðý adres
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyAllowSpecificOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
