using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using FunkoApi.Models.ViewModels;
using FunkoProject;
using FunkoProject.Data;
using FunkoProject.Data.Entities;
using NLog.Web;
using static FunkoProject.Services.AccountService;
using static FunkoProject.Services.FiguresService;
using FunkoProject.Middleware;
using FunkoProject.Models.Validators;
using Microsoft.Net.Http.Headers;

namespace FunkoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var authenticationSettings = new AuthenticationSettings();
            builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
            // Add services to the container.
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                };
            });
            builder.Services.AddSingleton(authenticationSettings);
            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IAccountServices, AccountServices>();
            builder.Services.AddScoped<IFiguresService, FiguresServices>();
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Host.UseNLog();

            var app = builder.Build();
            
            app.UseCors(policy =>
                policy.WithOrigins("https://localhost:7060")
                    .AllowAnyMethod()
                    .WithHeaders(HeaderNames.ContentType)
            );
            
            // Configure the HTTP request pipeline.
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();
            var scope = app.Services.CreateScope();
            // if (app.Environment.IsDevelopment())
            // {
            //     app.UseSwagger();
            //     app.UseSwaggerUI();
            // }
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}