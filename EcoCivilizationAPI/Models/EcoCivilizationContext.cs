using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EcoCivilizationAPI.Models;

public partial class EcoCivilizationContext : DbContext
{
    public EcoCivilizationContext()
    {
    }

    public EcoCivilizationContext(DbContextOptions<EcoCivilizationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<PhotoApplication> PhotoApplications { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-BLMHDHN\\SQL;Database=EcoCivilization;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.ToTable("Application");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountUser).HasColumnName("Count_User");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.IdCity).HasColumnName("ID_City");
            entity.Property(e => e.IdUser).HasColumnName("IDUser");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Place).IsUnicode(false);

            entity.HasOne(d => d.IdCityNavigation).WithMany(p => p.Applications)
                .HasForeignKey(d => d.IdCity)
                .HasConstraintName("FK_Application_City");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Applications)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Application_User");
        });

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Application_User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.IdApplication).HasColumnName("ID_Application");
            entity.Property(e => e.IdUser).HasColumnName("ID_User");

            entity.HasOne(d => d.IdApplicationNavigation).WithMany(p => p.ApplicationUsers)
                .HasForeignKey(d => d.IdApplication)
                .HasConstraintName("FK_Application_User_Application");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ApplicationUsers)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Application_User_User");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("Gender");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<PhotoApplication>(entity =>
        {
            entity.ToTable("PhotoApplication");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idapplicatioon).HasColumnName("IDApplicatioon");

            entity.HasOne(d => d.IdapplicatioonNavigation).WithMany(p => p.PhotoApplications)
                .HasForeignKey(d => d.Idapplicatioon)
                .HasConstraintName("FK_PhotoApplication_Application");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountApplication).HasColumnName("Count_Application");
            entity.Property(e => e.DateRegist).HasColumnType("date");
            entity.Property(e => e.IdCity).HasColumnName("ID_City");
            entity.Property(e => e.IdGender).HasColumnName("ID_Gender");
            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telephone)
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCityNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdCity)
                .HasConstraintName("FK_User_City");

            entity.HasOne(d => d.IdGenderNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdGender)
                .HasConstraintName("FK_User_Gender");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
