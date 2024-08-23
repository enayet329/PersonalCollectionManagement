using Microsoft.EntityFrameworkCore;
using PersonalCollectionManager.Domain.Entities;
using System;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Seed Users
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("d2c6e7b4-4a76-4b1e-8d8f-2b9f2f7e0e77"),
                Username = "admin",
                Email = "admin@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                ImageURL = "https://example.com/admin.jpg",
                PreferredLanguage = "en",
                PreferredThemeDark = false,
                IsAdmin = true,
                IsBlocked = false,
                JoinedAt = DateTime.UtcNow
            }
        );
            
    }
}
