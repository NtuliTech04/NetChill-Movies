using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetChill.Domain.Common;
using NetChill.Domain.Entities.Movie;
using System.Reflection.Emit;

namespace NetChill.Persistence.Configurations
{
    //Movie BaseInfo Model Configuration
    public class MovieBaseInfoConfiguration : IEntityTypeConfiguration<MovieBaseInfo>
    {
        public void Configure(EntityTypeBuilder<MovieBaseInfo> builder)
        {
            builder.HasKey(key => key.MovieId);

            builder.HasOne(rel => rel.MovieProduction)
             .WithOne(rel => rel.MovieBaseInfo)
             .HasForeignKey<MovieProduction>(fk => fk.MovieRef);

            builder.HasOne(rel => rel.MovieClip)
                .WithOne(rel => rel.MovieBaseInfo)
                .HasForeignKey<MovieClip>(fk => fk.MovieRef);

            builder.HasOne(rel => rel.TrackCreationProgress)
                .WithOne(rel => rel.MovieBaseInfo)
                .HasForeignKey<TrackCreationProgress>(fk => fk.MovieRef);

            //NotMapped Entity Properties
            builder.Ignore(p => p.BaseId);
        }
    }



    //Movie Production Model Configuration
    public class MovieProductionConfiguration : IEntityTypeConfiguration<MovieProduction>
    {
        public void Configure(EntityTypeBuilder<MovieProduction> builder)
        {
            builder.HasKey(key => key.ProductionId);

            //NotMapped Entity Properties
            builder.Ignore(p => p.BaseId);
        }
    }



    //Movie Clip Model Configuration
    public class MovieClipConfiguration : IEntityTypeConfiguration<MovieClip>
    {
        public void Configure(EntityTypeBuilder<MovieClip> builder)
        {
            builder.HasKey(key => key.ClipId);

            //NotMapped Entity Properties
            builder.Ignore(p => p.BaseId);
        }
    }



    //Movie Creation Progress Tracker Model Configuration
    public class CreationProgressConfiguration : IEntityTypeConfiguration<TrackCreationProgress>
    {
        public void Configure(EntityTypeBuilder<TrackCreationProgress> builder)
        {
            builder.HasKey(key => key.Id);

            //NotMapped Entity Properties
            builder.Ignore(p => p.BaseId);
        }
    }



    //Movie Genre Model Configuration
    public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
    {
        public void Configure(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.HasKey(key => key.GenreId);

            //NotMapped Entity Properties
            builder.Ignore(p => p.BaseId);
        }
    }



    //Movie Language Model Configuration
    public class MovieLanguageConfiguration : IEntityTypeConfiguration<MovieLanguage>
    {
        public void Configure(EntityTypeBuilder<MovieLanguage> builder)
        {
            builder.HasKey(key => key.LanguageId);

            //NotMapped Entity Properties
            builder.Ignore(p => p.BaseId);
        }
    }
}
