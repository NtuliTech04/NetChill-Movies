using Microsoft.EntityFrameworkCore;
using NetChill.Domain.Common;
using NetChill.Domain.Common.Interfaces;
using NetChill.Domain.Entities.Movie;
using System.Reflection;

namespace NetChill.Persistence.Contexts
{
    public class NetChillDbContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public NetChillDbContext(DbContextOptions<NetChillDbContext> options, IDomainEventDispatcher dispatcher)
            : base(options)
        {
            _dispatcher = dispatcher;
        }


        public DbSet<MovieBaseInfo> MovieBaseInfoes=> Set<MovieBaseInfo>();
        public DbSet<MovieProduction> MovieProductions => Set<MovieProduction>();
        public DbSet<MovieClip> MovieClips => Set<MovieClip>();
        public DbSet<TrackCreationProgress> CreationProgress => Set<TrackCreationProgress>();
        public DbSet<MovieGenre> Genres => Set<MovieGenre>();
        public DbSet<MovieLanguage> Languages => Set<MovieLanguage>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            if (_dispatcher == null) return result;

            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
