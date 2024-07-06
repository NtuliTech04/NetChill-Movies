using NetChill.Domain.ValueObjects;
using NetChill.Infrastructure.Identity.Services;
using NetChill.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediatR;
using NetChill.Domain.Common.Interfaces;
using NetChill.Domain.Common;
using NetChill.Application.Abstractions.Repositories.Authentication;
using NetChill.Application.Abstractions;

namespace NetChill.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.AddAppSettings(configuration);
            services.AddJWTAuthentication(configuration);
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IMediator, Mediator>();
            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAuthenticationResponseRepository, AuthenticationResponseService>();
        }


        //Sets configuration values in appsettings.json to the properties of value object classes
        private static void AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTValues>(configuration.GetSection("JWTValues"));
            services.Configure<EmailValues>(configuration.GetSection("EmailValues"));
        }

        //Configure DI for Json Web Token Authentication
        private static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => 
                { 
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["JWTValues:Issuer"],
                        ValidAudience = configuration["JWTValues:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTValues:Key"]))
                    };
                });
            services.AddAuthorization();
        }
    }
}
