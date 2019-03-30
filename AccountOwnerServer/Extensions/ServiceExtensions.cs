using Contracts;
using Entities;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AccountOwnerServer.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            // To allow HTTP communication with a client side such as Angular.
            services.AddCors(options =>
            {
                options.AddPolicy("AllowedPolicy",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowCredentials();
                });
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            // For deployment purposes only.
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["sqlserverconnection:connectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseSqlServer(connectionString));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureJWTAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = "http://localhost:5000",
                            ValidAudience = "http://localhost:5000",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                        };
                    });
        }
    }
}
