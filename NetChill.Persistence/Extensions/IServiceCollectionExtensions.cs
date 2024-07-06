using NetChill.Infrastructure.Identity.Models;
using NetChill.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using NetChill.Persistence.Repositories.Movie;
using NetChill.Persistence.Repositories;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Abstractions.Repositories.Movie;

namespace NetChill.Persistence.Extensions
{
    //Configures the dependencies defined in the Persistence layer. 
    public static class IServiceCollectionExtensions
    {
        public static void AddPersistentLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityContext(configuration);
            services.AddApplicationContext(configuration);
            services.AddIdentitySettings();
            services.AddRepositories();
            services.AddMappings();
        }


        private static void AddIdentityContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("NetChillUsersDevConnection");
            //var connectionString = configuration.GetConnectionString("NetChillUsersProdConnection"); 

            services.AddDbContext<IdentityDbContext>(options =>
              options.UseSqlServer(connectionString,
                 builder => builder.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName)));
        }

        private static void AddApplicationContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("NetChillMoviesDevConnection");
            //var connectionString = configuration.GetConnectionString("NetChillMoviesProdConnection"); 

            services.AddDbContext<NetChillDbContext>(options =>
                options.UseSqlServer(connectionString,
                   builder => builder.MigrationsAssembly(typeof(NetChillDbContext).Assembly.FullName)));
        }


        private static void AddIdentitySettings(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
        }


        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IMovieBaseInfoRepository, MovieBaseInfoRepository>();
            services.AddTransient<IMovieGenreRepository, MovieGenreRepository>();
            services.AddTransient<IMovieLanguageRepository, MovieLanguageRepository>();
            services.AddTransient<IMovieProductionRepository, MovieProductionRepository>();
            services.AddTransient<IMovieClipRepository, MovieClipRepository>();
            services.AddTransient<ITrackCreationProgressRepository, TrackCreationProgressRepository>();
        }


        private static void AddMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
