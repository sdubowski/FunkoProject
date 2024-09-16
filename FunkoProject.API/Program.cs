using System.Text;
using Amazon;
using Amazon.S3;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using FunkoApi.Models.ViewModels;
using FunkoProject;
using FunkoProject.Data;
using FunkoProject.Data.Entities;
using FunkoProject.Extensions;
using NLog.Web;
using FunkoProject.Middleware;
using FunkoProject.Models.Validators;
using FunkoProject.Repositories;
using FunkoProject.Services;
using FunkoProject.Services.Validation;
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
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
            builder.Services.AddAWSService<IAmazonS3>();
            builder.Services.AddSingleton<IAmazonS3>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var accessKey = configuration["AWS:AccessKey"];
                var secretKey = configuration["AWS:SecretKey"];
                var region = RegionEndpoint.GetBySystemName(configuration["AWS:Region"]);
                return new AmazonS3Client(accessKey, secretKey, region);
            });
            builder.Services.AddScoped<IUserValidationService, UserValidationService>();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureAutoMapper();
            builder.Host.UseNLog();

            var app = builder.Build();
            
            var mapper = app.Services.GetRequiredService<IMapper>();
            MappingExtensions.MapperAccessor.Configure(mapper);
            
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