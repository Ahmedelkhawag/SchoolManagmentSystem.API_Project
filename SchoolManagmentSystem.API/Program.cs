
using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Core.CoreDependacies;
using SchoolManagmentSystem.Infrastructure.Data;
using SchoolManagmentSystem.Infrastructure.Dependacies;
using SchoolManagmentSystem.Service.Dependacies;


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

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    options.RoutePrefix = string.Empty; // ?? ???? ????? Swagger ??? ?????? ????????
                });

            }

            app.UseHttpsRedirection();

               app.UseAuthorization();


              app.MapControllers();

              app.Run();
        }
    }
}
