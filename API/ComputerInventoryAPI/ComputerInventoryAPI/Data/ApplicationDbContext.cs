using ComputerInventoryAPI.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OperatingSystem = ComputerInventoryAPI.Models.Entities.OperatingSystem;

namespace ComputerInventoryAPI.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Computer>? Computer { get; set; }
    public DbSet<Brand>? Brand { get; set; }
    public DbSet<OperatingSystem>? OperatingSystems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // SEEDING PREPARATION
        var hasher = new PasswordHasher<IdentityUser>();

        var adminUser = new IdentityUser
        {
            UserName = "admin@example.com",
            NormalizedUserName = "ADMIN@EXAMPLE.COM",
            Email = "admin@example.com",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "AdminPassword123!"),
            SecurityStamp = string.Empty
        };

        // SEEDING
        builder.Entity<IdentityUser>().HasData(adminUser);

        // Brands
        builder.Entity<Brand>().HasData(new Brand { Id = 1, Name = "Apple" });
        builder.Entity<Brand>().HasData(new Brand { Id = 2, Name = "Asus" });
        builder.Entity<Brand>().HasData(new Brand { Id = 3, Name = "Dell" });
        builder.Entity<Brand>().HasData(new Brand { Id = 4, Name = "Samsung" });
        builder.Entity<Brand>().HasData(new Brand { Id = 5, Name = "HP" });

        // OS
        builder.Entity<OperatingSystem>()
            .HasData(new OperatingSystem { Id = 1, Name = "MacOS", Version = "15 Sequoia" });
        builder.Entity<OperatingSystem>()
            .HasData(new OperatingSystem { Id = 2, Name = "MacOS", Version = "14 Sonoma" });
        builder.Entity<OperatingSystem>()
            .HasData(new OperatingSystem { Id = 3, Name = "MacOS", Version = "13 Ventura" });

        builder.Entity<OperatingSystem>().HasData(new OperatingSystem { Id = 4, Name = "Windows", Version = "11" });
        builder.Entity<OperatingSystem>().HasData(new OperatingSystem { Id = 5, Name = "Windows", Version = "10" });
        builder.Entity<OperatingSystem>().HasData(new OperatingSystem { Id = 6, Name = "Windows", Version = "Vista" });
        builder.Entity<OperatingSystem>().HasData(new OperatingSystem { Id = 7, Name = "Windows", Version = "XP" });

        builder.Entity<OperatingSystem>().HasData(new OperatingSystem
            { Id = 8, Name = "Linux", Version = "Ubuntu 20.04" });
        builder.Entity<OperatingSystem>()
            .HasData(new OperatingSystem { Id = 9, Name = "Linux", Version = "Fedora 34" });

        builder.Entity<OperatingSystem>().HasData(new OperatingSystem { Id = 10, Name = "Chrome OS", Version = "92" });

        // Computers
        builder.Entity<Computer>().HasData(new Computer
        {
            Id = 1,
            Name = "roy_espen_mini_m2",
            Processor = "M2",
            Ram = 24,
            Storage = 512,
            OperatingSystemId = 2,
            BrandId = 1
        });
        builder.Entity<Computer>().HasData(new Computer
        {
            Id = 2,
            Name = "awesome_gaming_pc",
            Processor = "Ryzen 9",
            Ram = 64,
            Storage = 1024,
            OperatingSystemId = 4,
            BrandId = 2
        });
        builder.Entity<Computer>().HasData(new Computer
        {
            Id = 3,
            Name = "workhorse_laptop",
            Processor = "Intel i7",
            Ram = 16,
            Storage = 512,
            OperatingSystemId = 8,
            BrandId = 3
        });
        builder.Entity<Computer>().HasData(new Computer
        {
            Id = 4,
            Name = "student_chromebook",
            Processor = "ARM Cortex",
            Ram = 4,
            Storage = 64,
            OperatingSystemId = 10,
            BrandId = 4
        });
    }
}

/*
SELECT c.Name, c.Processor, c.Ram, c.Storage, b.Name AS Brand, os.Name AS OperatingSystem, os.Version AS OS_Version
FROM Computer c
JOIN OperatingSystems os ON c.OperatingSystemId = os.Id
JOIN Brand b ON c.BrandId = b.Id;
*/