using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ForOffer.Models.DB
{
    public partial class ForOfferContext : DbContext
    {
        public ForOfferContext()
        {
        }

        public ForOfferContext(DbContextOptions<ForOfferContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MfGeneralNotesOffer> MfGeneralNotesOffers { get; set; }
        public virtual DbSet<MfGeneralObservationsOffer> MfGeneralObservationsOffers { get; set; }
        public virtual DbSet<MfServicesPlace> MfServicesPlaces { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=ForOffer;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<MfGeneralNotesOffer>(entity =>
            {
                entity.HasKey(e => e.GeneralNotesOfferId);

                entity.ToTable("mf_general_notes_offer");

                entity.Property(e => e.GeneralNotesOfferId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("general_notes_offer_id");

                entity.Property(e => e.GeneralNotesServicesId).HasColumnName("general_notes_services_id");

                entity.Property(e => e.GeneralNotesText)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("general_notes_text");

                entity.Property(e => e.RegState).HasColumnName("reg_state");

                //entity.HasOne(d => d.GeneralNotesOffer)
                //    .WithOne(p => p.MfGeneralNotesOffer)
                //    .HasForeignKey<MfGeneralNotesOffer>(d => d.GeneralNotesOfferId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_mf_general_notes_offer_mf_services_place");
            });

            modelBuilder.Entity<MfGeneralObservationsOffer>(entity =>
            {
                entity.HasKey(e => e.GeneralObservationOfferId);

                entity.ToTable("mf_general_observations_offer");

                entity.Property(e => e.GeneralObservationOfferId).HasColumnName("general_observation_offer_id");

                entity.Property(e => e.GeneralObservationServicesId).HasColumnName("general_observation_services_id");

                entity.Property(e => e.GeneralObservationText)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("general_observation_text");

                entity.Property(e => e.RegState).HasColumnName("reg_state");

                //entity.HasOne(d => d.GeneralObservationServices)
                //    .WithMany(p => p.MfGeneralObservationsOffers)
                //    .HasForeignKey(d => d.GeneralObservationServicesId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_mf_general_observations_offer_mf_services_place");
            });

            modelBuilder.Entity<MfServicesPlace>(entity =>
            {
                entity.HasKey(e => e.ServicesPlaceId);

                entity.ToTable("mf_services_place");

                entity.Property(e => e.ServicesPlaceId).HasColumnName("services_place_id");

                entity.Property(e => e.ServicesPlaces)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("services_places");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
