
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using SchoolManagmentSystem.Core.CoreDependacies;
using SchoolManagmentSystem.Core.Middlewares;
using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Infrastructure.Data;
using SchoolManagmentSystem.Infrastructure.Dependacies;
using SchoolManagmentSystem.Infrastructure.SeedingData;
using SchoolManagmentSystem.Service.Dependacies;
using System.Globalization;


namespace SchoolManagmentSystem.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region builder object

            var builder = WebApplication.CreateBuilder(args);
            #endregion

            // Add services to the container.
            #region Controller Services

            builder.Services.AddControllers();

            #endregion
            #region DbContext Services

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CS")));

            #endregion
            #region Identity Services
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            #endregion
            #region InfraStructure Services

            builder.Services.AddInfrastructureDependacies();


            #endregion
            #region Service Layer Services
            builder.Services.AddServiceDependacies();
            #endregion
            #region Core Services
            builder.Services.AddCoreDependacies();
            #endregion
            #region Swagger service

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #endregion
            #region Localization 
            builder.Services.AddLocalization(options =>
            {
                options.ResourcesPath = "";
            }
            );

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("ar-EG")
            };
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
            });
            #endregion
            #region JWT Service
            builder.Services.AddJWTServiceRegistration(builder.Configuration);
            #endregion

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            #region open api service

            builder.Services.AddOpenApi();
            #endregion
            #region App obj
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                await UserSeeding.SeedUsers(userManager);
                await RoleSeeding.SeedRoles(roleManager);
            }
            #endregion

            // Configure the HTTP request pipeline.
            #region HTTP request pipeline

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    options.RoutePrefix = string.Empty; // ?? ???? ????? Swagger ??? ?????? ????????

                });

                app.MapScalarApiReference();


            }
            app.UseMiddleware<ErrorHandlerMiddleware>();
            var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion

        }
    }
}
