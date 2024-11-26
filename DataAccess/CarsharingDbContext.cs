using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class CarsharingDbContext : DbContext
{
    public CarsharingDbContext(DbContextOptions<CarsharingDbContext> options) : base(options)
    {
    }

    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<DriverLicenseEntity> DriverLicenses { get; set; }
    public DbSet<RentEntity> Rents { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<EmployeeEntity> Employers { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<AdminEntity> Admin { get; set; }
    public DbSet<StateEntity> States { get; set; }
    public DbSet<TechnicalInspectionEntity> TechnicalInspections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("user_claims");
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("user_logins").HasNoKey();
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable("user_tokens").HasNoKey();
        modelBuilder.Entity<IdentityRole<int>>().ToTable("user_roles");
        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("user_roles_claims");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("user_role_owners").HasNoKey();
        
        modelBuilder.Entity<UserEntity>().ToTable("Users");
        modelBuilder.Entity<CustomerEntity>().ToTable("Customers");
        modelBuilder.Entity<EmployeeEntity>().ToTable("Employees");
        modelBuilder.Entity<AdminEntity>().ToTable("Admins");
        
        modelBuilder.Entity<UserEntity>().HasIndex(x => x.Login).IsUnique();
        modelBuilder.Entity<EmployeeEntity>().HasIndex(x => x.ExternalId).IsUnique();
        modelBuilder.Entity<CustomerEntity>().HasIndex(x => x.ExternalId).IsUnique();
        
        modelBuilder.Entity<CarEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<DriverLicenseEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<RentEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<StateEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<TechnicalInspectionEntity>().HasKey(x => x.Id);
        
        modelBuilder.Entity<CarEntity>()
            .HasOne(x => x.State)
            .WithMany(x => x.Cars)
            .HasForeignKey(x => x.StateId);

        modelBuilder.Entity<UserEntity>()
            .HasOne(x => x.State)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.StateId);

        modelBuilder.Entity<DriverLicenseEntity>()
            .HasOne(x => x.Customer)
            .WithMany(x => x.DriverLicenses)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<RentEntity>()
            .HasOne(x => x.Customer)
            .WithMany(x => x.Rents)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<RentEntity>()
            .HasOne(x => x.State)
            .WithMany(x => x.Rents)
            .HasForeignKey(x => x.StateId);

        modelBuilder.Entity<RentEntity>()
            .HasOne(x => x.Car)
            .WithMany(x => x.Rents)
            .HasForeignKey(x => x.CarId);

        modelBuilder.Entity<TechnicalInspectionEntity>()
            .HasOne(x => x.Car)
            .WithMany(x => x.TechnicalInspections)
            .HasForeignKey(x => x.CarId);

        modelBuilder.Entity<TechnicalInspectionEntity>()
            .HasOne(x => x.TechnicalEmployee)
            .WithMany(x => x.TechnicalInspections)
            .HasForeignKey(x => x.TechnicianId);

        modelBuilder.Entity<TechnicalInspectionEntity>()
            .HasOne(x => x.State)
            .WithMany(x => x.TechnicalInspections)
            .HasForeignKey(x => x.MaintenanceStateId);
    }
}
