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
                Email = "admin@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@1234"),
                IsAdmin = true,
                IsBlocked = false
            },
            new User
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Username = "user1",
                Email = "user1@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User@1234"),
                IsAdmin = false,
                IsBlocked = false
            }
        );

        // Seed Collections
        modelBuilder.Entity<Collection>().HasData(
            new Collection
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Collection 1",
                Description = "First collection",
                Topic = "General",
                ImageUrl = "https://example.com/collection1.jpg",
                UserId = Guid.Parse("11111111-1111-1111-1111-111111111111")
            }
        );

        // Seed Items
        modelBuilder.Entity<Item>().HasData(
            new Item
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Item 1",
                Description = "First item",
                ImgUrl = "https://example.com/item1.jpg",
                CollectionId = Guid.Parse("22222222-2222-2222-2222-222222222222")
            }
        );

        // Seed Tags
        modelBuilder.Entity<Tag>().HasData(
            new Tag
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Name = "Tag 1",
                ItemId = Guid.Parse("33333333-3333-3333-3333-333333333333")
            }
        );

        // Seed Comments
        modelBuilder.Entity<Comment>().HasData(
            new Comment
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Content = "First comment",
                CreatedAt = DateTime.UtcNow,
                UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ItemId = Guid.Parse("33333333-3333-3333-3333-333333333333")
            }
        );

        // Seed Likes
        modelBuilder.Entity<Like>().HasData(
            new Like
            {
                Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ItemId = Guid.Parse("33333333-3333-3333-3333-333333333333")
            }
        );
    }
}
