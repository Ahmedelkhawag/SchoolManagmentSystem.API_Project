
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using SchoolManagmentSystem.Core.CoreDependacies;
using SchoolManagmentSystem.Core.Middlewares;
using SchoolManagmentSystem.Infrastructure.Data;
using SchoolManagmentSystem.Infrastructure.Dependacies;
using SchoolManagmentSystem.Service.Dependacies;
using System.Globalization;



namespace SchoolManagmentSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Controller Services

            builder.Services.AddControllers();

            #endregion
            #region DbContext Services

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CS")));

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

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion

        }
    }
}
