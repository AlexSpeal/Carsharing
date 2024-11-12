using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class CarsharingDbContext : DbContext
{
    public CarsharingDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<DriverLicenseEntity> DriverLicenses { get; set; }
    public DbSet<RentEntity> Rents { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<StateEntity> States { get; set; }
    public DbSet<TechnicalInspectionEntity> TechnicalInspections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TechnicalInspectionEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<CarEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<DriverLicenseEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<RentEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<RoleEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<StateEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);

        modelBuilder.Entity<CarEntity>().HasOne(x => x.State)
            .WithMany(x => x.Cars).HasForeignKey(x => x.StateId);

        modelBuilder.Entity<UserEntity>().HasOne(x => x.Role)
            .WithMany(x => x.Users).HasForeignKey(x => x.RoleId);

        modelBuilder.Entity<UserEntity>().HasOne(x => x.State)
            .WithMany(x => x.Users).HasForeignKey(x => x.StateId);

        modelBuilder.Entity<DriverLicenseEntity>().HasOne(x=>x.User)
            .WithMany(x=>x.DriverLicenses).HasForeignKey(x=>x.UserId);

        modelBuilder.Entity<RentEntity>().HasOne(x => x.User)
            .WithMany(x => x.Rents).HasForeignKey(x => x.UserId);

        modelBuilder.Entity<RentEntity>().HasOne(x => x.State)
            .WithMany(x => x.Rents).HasForeignKey(x => x.StateId);

        modelBuilder.Entity<RentEntity>().HasOne(x => x.Car)
            .WithMany(x => x.Rents).HasForeignKey(x => x.CarId);

        modelBuilder.Entity<TechnicalInspectionEntity>().HasOne(x => x.Car)
            .WithMany(x => x.TechnicalInspections).HasForeignKey(x => x.CarId);

        modelBuilder.Entity<TechnicalInspectionEntity>().HasOne(x => x.TechnicalEmployee)
            .WithMany(x => x.TechnicalInspections).HasForeignKey(x => x.TechnicianId);

        modelBuilder.Entity<TechnicalInspectionEntity>().HasOne(x => x.State)
            .WithMany(x => x.TechnicalInspections).HasForeignKey(x => x.MaintenanceStateId);
    }
}