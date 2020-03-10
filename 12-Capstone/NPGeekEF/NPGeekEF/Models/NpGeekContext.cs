using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NPGeekEF.Models
{
    public partial class NpGeekContext : DbContext
    {
        public NpGeekContext()
        {
        }

        public NpGeekContext(DbContextOptions<NpGeekContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Park> Park { get; set; }
        public virtual DbSet<SurveyResult> SurveyResult { get; set; }
        public virtual DbSet<Weather> Weather { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=NpGeek;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Park>(entity =>
            {
                entity.HasKey(e => e.ParkCode)
                    .HasName("pk_park");

                entity.ToTable("park");

                entity.Property(e => e.ParkCode)
                    .HasColumnName("parkCode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Acreage).HasColumnName("acreage");

                entity.Property(e => e.AnnualVisitorCount).HasColumnName("annualVisitorCount");

                entity.Property(e => e.Climate)
                    .IsRequired()
                    .HasColumnName("climate")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ElevationInFeet).HasColumnName("elevationInFeet");

                entity.Property(e => e.EntryFee).HasColumnName("entryFee");

                entity.Property(e => e.InspirationalQuote)
                    .IsRequired()
                    .HasColumnName("inspirationalQuote")
                    .IsUnicode(false);

                entity.Property(e => e.InspirationalQuoteSource)
                    .IsRequired()
                    .HasColumnName("inspirationalQuoteSource")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MilesOfTrail).HasColumnName("milesOfTrail");

                entity.Property(e => e.NumberOfAnimalSpecies).HasColumnName("numberOfAnimalSpecies");

                entity.Property(e => e.NumberOfCampsites).HasColumnName("numberOfCampsites");

                entity.Property(e => e.ParkDescription)
                    .IsRequired()
                    .HasColumnName("parkDescription")
                    .IsUnicode(false);

                entity.Property(e => e.ParkName)
                    .IsRequired()
                    .HasColumnName("parkName")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.YearFounded).HasColumnName("yearFounded");
            });

            modelBuilder.Entity<SurveyResult>(entity =>
            {
                entity.HasKey(e => e.SurveyId)
                    .HasName("pk_survey_result");

                entity.ToTable("survey_result");

                entity.Property(e => e.SurveyId).HasColumnName("surveyId");

                entity.Property(e => e.ActivityLevel)
                    .IsRequired()
                    .HasColumnName("activityLevel")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("emailAddress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParkCode)
                    .IsRequired()
                    .HasColumnName("parkCode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.ParkCodeNavigation)
                    .WithMany(p => p.SurveyResult)
                    .HasForeignKey(d => d.ParkCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_survey_result_park");
            });

            modelBuilder.Entity<Weather>(entity =>
            {
                entity.HasKey(e => new { e.ParkCode, e.FiveDayForecastValue })
                    .HasName("pk_weather");

                entity.ToTable("weather");

                entity.Property(e => e.ParkCode)
                    .HasColumnName("parkCode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FiveDayForecastValue).HasColumnName("fiveDayForecastValue");

                entity.Property(e => e.Forecast)
                    .IsRequired()
                    .HasColumnName("forecast")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.High).HasColumnName("high");

                entity.Property(e => e.Low).HasColumnName("low");

                entity.HasOne(d => d.ParkCodeNavigation)
                    .WithMany(p => p.Weather)
                    .HasForeignKey(d => d.ParkCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_weather_park");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
