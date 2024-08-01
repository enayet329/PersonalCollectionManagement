using Microsoft.EntityFrameworkCore;
using PersonalCollectionManager.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PersonalCollectionManager.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Indexing for User entity
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Indexing for Like entity
            modelBuilder.Entity<Like>()
                .HasIndex(l => new { l.UserId, l.ItemId })
                .IsUnique();

            // Relationships between entities
            modelBuilder.Entity<Collection>()
                .HasOne(c => c.User)
                .WithMany(u => u.Collections)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Item)
                .WithMany(i => i.Comments)
                .HasForeignKey(c => c.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Collection)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CollectionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Item)
                .WithMany(i => i.Likes)
                .HasForeignKey(l => l.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.Items)
                .WithMany(i => i.Tags)
                .UsingEntity<Dictionary<string, object>>(
                    "ItemTags",
                    j => j.HasOne<Item>().WithMany().HasForeignKey("ItemId"),
                    j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"));

            // Configuring the length constraints for strings
            modelBuilder.Entity<Collection>()
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Item>()
                .Property(i => i.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<Tag>()
                .Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(c => c.Content)
                .IsRequired();

            // Seed admin user with hashed password
            var adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@1234");

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("d2c6e7b4-4a76-4b1e-8d8f-2b9f2f7e0e77"),
                    Username = "admin",
                    Email = "admin@example.com",
                    PasswordHash = adminPasswordHash,
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

            modelBuilder.Entity<Collection>().HasData(
                new Collection
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                    Name = "Collection 1",
                    UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Topic = "Topic 1",
                    Description = "description 1",
                    ImageUrl = "https://example.com/image1.jpg"
                }
            );

            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111113"),
                    Name = "Item 1",
                    CollectionId = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                    ImgUrl = "https://example.com/image1.jpg",
                }
            );

            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111114"),
                    Name = "Tag 1"
                }
            );

            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111115"),
                    Content = "Comment 1",
                    UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    ItemId = Guid.Parse("11111111-1111-1111-1111-111111111113")
                }
            );

            modelBuilder.Entity<Like>().HasData(
                new Like
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111116"),
                    UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    ItemId = Guid.Parse("11111111-1111-1111-1111-111111111113")
                }
            );

            // Seed many-to-many relationships
            modelBuilder.Entity("ItemTags").HasData(
                new { ItemId = Guid.Parse("11111111-1111-1111-1111-111111111113"), TagId = Guid.Parse("11111111-1111-1111-1111-111111111114") }
            );
        }
    }
}
