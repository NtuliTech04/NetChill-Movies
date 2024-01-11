using Microsoft.OpenApi.Models;
using System.Reflection;

namespace NetChill.WebAPI.UI.Extensions
{
    public static class IServiceCollectionExtensions
    {
        //Configures the dependencies defined in the Presentation layer. 
        public static void AddPresentationLayer(this IServiceCollection services)
        {
            services.CorsPolicy();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerService();
        }


        //Configures client app access by whitelisting its URL
        public static void CorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", 
                    policy => policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        //Swagger API Configurations
        //Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        private static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "NetChill Movies",
                    Description = "Movie streaming using Angular and ASP.NET Core Web API"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                //Project SDK Configuration
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}