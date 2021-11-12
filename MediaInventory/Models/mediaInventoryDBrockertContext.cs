﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MediaInventory.Models
{
    public partial class mediaInventoryDBrockertContext : DbContext
    {
        public mediaInventoryDBrockertContext()
        {
        }

        public mediaInventoryDBrockertContext(DbContextOptions<mediaInventoryDBrockertContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<ArtistType> ArtistTypes { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<MediaHasArtist> MediaHasArtists { get; set; }
        public virtual DbSet<MediaHasBorrower> MediaHasBorrowers { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<Medium> Media { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<ViewIndividualArtist> ViewIndividualArtists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=.\\SQLDEV01;Database=mediaInventoryDBrockert;Trusted_Connection=True;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("artist");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.ArtistName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("artist_name");

                entity.Property(e => e.ArtistTypeId).HasColumnName("artist_type_id");

                entity.HasOne(d => d.ArtistType)
                    .WithMany(p => p.Artists)
                    .HasForeignKey(d => d.ArtistTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__artist__artist_t__2E1BDC42");
            });

            modelBuilder.Entity<ArtistType>(entity =>
            {
                entity.ToTable("artist_type");

                entity.Property(e => e.ArtistTypeId).HasColumnName("artist_type_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Borrower>(entity =>
            {
                entity.ToTable("borrower");

                entity.Property(e => e.BorrowerId).HasColumnName("borrower_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("first_name")
                    .IsFixedLength(true);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("last_name")
                    .IsFixedLength(true);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("phone_number")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genre");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<MediaHasArtist>(entity =>
            {
                entity.HasKey(e => e.MediaHasArtist1)
                    .HasName("PK__media_ha__34D332716D1FC429");

                entity.ToTable("media_has_artist");

                entity.Property(e => e.MediaHasArtist1).HasColumnName("media_has_artist");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.MediaId).HasColumnName("media_id");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.MediaHasArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__media_has__artis__398D8EEE");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.MediaHasArtists)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__media_has__media__3A81B327");
            });

            modelBuilder.Entity<MediaHasBorrower>(entity =>
            {
                entity.ToTable("media_has_borrower");

                entity.Property(e => e.MediaHasBorrowerId).HasColumnName("media_has_borrower_id");

                entity.Property(e => e.BorrowDate)
                    .HasColumnType("date")
                    .HasColumnName("borrow_date");

                entity.Property(e => e.BorrowerId).HasColumnName("borrower_id");

                entity.Property(e => e.MediaId).HasColumnName("media_id");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("date")
                    .HasColumnName("return_date");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.MediaHasBorrowers)
                    .HasForeignKey(d => d.BorrowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__media_has__borro__35BCFE0A");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.MediaHasBorrowers)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__media_has__media__36B12243");
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.ToTable("media_type");

                entity.Property(e => e.MediaTypeId).HasColumnName("media_type_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Medium>(entity =>
            {
                entity.HasKey(e => e.MediaId)
                    .HasName("PK__media__D0A840F4BACE24E2");

                entity.ToTable("media");

                entity.Property(e => e.MediaId).HasColumnName("media_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.MediaName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("media_name");

                entity.Property(e => e.MediaTypeId).HasColumnName("media_type_id");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date")
                    .HasColumnName("release_date");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__media__genre_id__32E0915F");

                entity.HasOne(d => d.MediaType)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.MediaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__media__media_typ__30F848ED");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__media__status_id__31EC6D26");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ViewIndividualArtist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Individual_Artist");

                entity.Property(e => e.ArtistId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("artist_id");

                entity.Property(e => e.ArtistName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("artist_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
