using System;
using System.Text;
using Api.Data;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Lấy cấu hình
var configuration = builder.Configuration;

// Thêm các dịch vụ vào container
builder.Services.AddControllers();

// Cấu hình DbContext
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("MyDb"));
});

// Cấu hình Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
});

// Đăng ký các dịch vụ (DI)
builder.Services.AddScoped<ILoaiRepository, LoaiRepositoryInMemory>();
builder.Services.AddScoped<IHangHoaRepository, HangHoaRepository>();

// Cấu hình AppSettings
builder.Services.Configure<AppSetting>(configuration.GetSection("AppSettings"));

// Cấu hình JWT Authentication
var secretKey = configuration["Appsettings:SecretKey"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, // Không cần xác thực Issuer
            ValidateAudience = false, // Không cần xác thực Audience
            ValidateIssuerSigningKey = true, // Xác thực chữ ký
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
            ClockSkew = TimeSpan.Zero // Không cho phép trễ thời gian
        };
    });

var app = builder.Build();

// Cấu hình HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
