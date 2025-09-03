using CupcakeMVC.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CupcakeMVC.Data;

// dotnet ef migrations add <name of migration>
// dotnet ef database update

public class CupcakeDbContext(DbContextOptions<CupcakeDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Cupcake>().ToTable("Cupcakes");
        
        // SEEDING PREPARATION
        var hasher = new PasswordHasher<IdentityUser>();
        var defaultUser = new IdentityUser
        {
            Id = "default-id",
            UserName = "default@example.com",
            NormalizedUserName = "DEFAULT@EXAMPLE.COM",
            Email = "default@example.com",
            NormalizedEmail = "DEFAULT@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "DefaultPassword123!"), //Passwords should in general not be written like this. Recommended to use env or similar.
            SecurityStamp = string.Empty
        };
        
        // SEEDING 
        builder.Entity<IdentityUser>().HasData(defaultUser);
        
        // Sizes
        builder.Entity<Size>().HasData(new Size { Id = 1, Name = "Bite Size" });
        builder.Entity<Size>().HasData(new Size { Id = 2, Name = "Small" });
        builder.Entity<Size>().HasData(new Size { Id = 3, Name = "Medium" });
        builder.Entity<Size>().HasData(new Size { Id = 4, Name = "Large" });
        builder.Entity<Size>().HasData(new Size { Id = 5, Name = "Almost Cake Size" });
        
        // Categories
        builder.Entity<Category>().HasData(new Category { Id = 1, Name = "Regular" });
        builder.Entity<Category>().HasData(new Category { Id = 2, Name = "Vegan" });
        builder.Entity<Category>().HasData(new Category { Id = 3, Name = "Gluten Free" });
        
        // Cupcakes: cupcakes by https://www.ediblearrangements.com/blog/types-of-cupcakes/
        builder.Entity<Cupcake>().HasData(new Cupcake 
        { 
            Id = 1, 
            Name = "Strawberry Shortcake", 
            Description = "Stuffed with fresh strawberries and topped with fluffy whipped cream, these strawberry shortcake cupcakes are simply divine. Plus, the vanilla cupcake batter couldn’t be easier to whip up.", 
            SizeId = 3, 
            CategoryId = 1,
            OwnerId = defaultUser.Id
        });
        builder.Entity<Cupcake>().HasData(new Cupcake 
        { 
            Id = 2, 
            Name = "Lemon Cupcakes", 
            Description = "Filled with lemon curd and topped with lemon buttercream frosting, these cupcakes are sweet, tangy, and jam-packed with flavor. They taste like lemon drop candies in cupcake form.", 
            SizeId = 3, 
            CategoryId = 1,
            OwnerId = defaultUser.Id
        });
        builder.Entity<Cupcake>().HasData(new Cupcake 
        { 
            Id = 3, 
            Name = "Chocolate Cupcakes with Peanut Butter Frosting", 
            Description = "These cupcakes are the ultimate chocolate and peanut butter dessert. A dollop of peanut butter frosting tops moist chocolate cupcakes with chocolate drizzle and mini peanut butter cups.These cupcakes are the ultimate chocolate and peanut butter dessert. A dollop of peanut butter frosting tops moist chocolate cupcakes with chocolate drizzle and mini peanut butter cups.", 
            SizeId = 2, 
            CategoryId = 1,
            OwnerId = defaultUser.Id
        });
        builder.Entity<Cupcake>().HasData(new Cupcake 
        { 
            Id = 4, 
            Name = "Coconut Cupcakes", 
            Description = "Have a penchant for coconut? These cupcakes are made with a soft, fluffy vanilla cake topped with a coconut cream cheese buttercream frosting. Toasted coconut sprinkled on top makes the entire cupcake tastier.", 
            SizeId = 2, 
            CategoryId = 2,
            OwnerId = defaultUser.Id
        });
    }
    
    public DbSet<IdentityUser>? Users { get; set; }
    public DbSet<Size>? Sizes { get; set; }
    public DbSet<Cupcake>? Cupcakes { get; set; }
    public DbSet<Category>? Categories { get; set; }
}