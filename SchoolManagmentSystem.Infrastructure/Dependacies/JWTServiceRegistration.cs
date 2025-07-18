﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolManagmentSystem.Data.Helpers;
using System.Text;

namespace SchoolManagmentSystem.Infrastructure.Dependacies
{
    public static class JWTServiceRegistration
    {
        public static IServiceCollection AddJWTServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<JWT>(configuration.GetSection("JWT"));
            var jwtSettings = configuration.GetSection("JWT").Get<JWT>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifetime,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "School Project", Version = "v1" });
                c.EnableAnnotations();

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
            }
           });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanViewUsers", policy => policy.RequireRole("Admin").RequireClaim("Permissions", "CanViewUsers"));
                options.AddPolicy("CanViewDashboard", policy => policy.RequireRole("Admin").RequireClaim("Permissions", "CanViewDashboard"));
                options.AddPolicy("CanEditDashboard", policy => policy.RequireRole("Admin").RequireClaim("Permissions", "CanEditDashboard"));
                options.AddPolicy("CanViewRoles", policy => policy.RequireRole("Admin").RequireClaim("Permissions", "CanViewRoles"));
                options.AddPolicy("CanEditRoles", policy => policy.RequireRole("Admin").RequireClaim("Permissions", "CanEditRoles"));
                options.AddPolicy("CanDeleteRoles", policy => policy.RequireRole("Admin").RequireClaim("Permissions", "CanDeleteRoles"));
                options.AddPolicy("CanCreateRoles", policy => policy.RequireRole("Admin").RequireClaim("Permissions", "CanCreateRoles"));
                options.AddPolicy("CanViewUsers", policy => policy.RequireRole("Admin").RequireClaim("Permissions", "CanViewUsers"));
                options.AddPolicy("CanEditUsers", policy => policy.RequireRole("Admin").RequireClaim("Permissions", "CanEditUsers"));
                options.AddPolicy("CanDeleteUsers", policy => policy.RequireRole("Admin").RequireClaim("Permissions", "CanDeleteUsers"));
                options.AddPolicy("CanCreateUsers", policy => policy.RequireRole("Admin").RequireClaim("Permissions", "CanCreateUsers"));
                options.AddPolicy("CanViewUserClaims", policy => policy.RequireRole("Admin").RequireClaim("Permissions", "CanViewUserClaims"));
                options.AddPolicy("CanEditUserClaims", policy => policy.RequireRole("Admin").RequireClaim("Permissions", "CanEditUserClaims"));



            });



            return services;
        }
    }
}
