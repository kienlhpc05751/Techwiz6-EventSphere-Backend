using System;
using System.Collections.Generic;
using EventSphere.Entity;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace EventSphere.Db;

public partial class EventSphereDbContext : DbContext
{
    public EventSphereDbContext()
    {
    }

    public EventSphereDbContext(DbContextOptions<EventSphereDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventsSeatsView> EventsSeatsViews { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<MediaGallery> MediaGalleries { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PRIMARY");

            entity.Property(e => e.RegistrationId).ValueGeneratedNever();
            entity.Property(e => e.MarkedOn).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.Registration).WithOne(p => p.Attendance).HasConstraintName("attendance_registrations_id_fk");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PRIMARY");

            entity.Property(e => e.AttendanceId).ValueGeneratedNever();
            entity.Property(e => e.IssuedOn).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.Attendance).WithOne(p => p.Certificate).HasConstraintName("certificates_attendance_registration_id_fk");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Events)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("events_users_id_fk");

            entity.HasOne(d => d.Venue).WithMany(p => p.Events)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("events_venues_id_fk");
        });

        modelBuilder.Entity<EventsSeatsView>(entity =>
        {
            entity.ToView("events_seats_view");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.SubmittedOn).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.Registration).WithMany(p => p.Feedbacks).HasConstraintName("feedback_registrations_id_fk");
        });

        modelBuilder.Entity<MediaGallery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.UploadedOn).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.Event).WithMany(p => p.MediaGalleries).HasConstraintName("media_gallery_events_id_fk");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.MediaGalleries)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("media_gallery_users_id_fk");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Status).HasDefaultValueSql("'pending'");

            entity.HasOne(d => d.Event).WithMany(p => p.Registrations).HasConstraintName("registrations_events_id_fk");

            entity.HasOne(d => d.Student).WithMany(p => p.Registrations)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("registrations_users_id_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp()");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.User).WithOne(p => p.UserDetail).HasConstraintName("user_details_users_id_fk");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
