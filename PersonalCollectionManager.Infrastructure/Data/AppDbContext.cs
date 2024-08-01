using Microsoft.EntityFrameworkCore;
using PersonalCollectionManager.Domain.Entities;
using System;

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
            modelBuilder.Entity<User>()
                .HasMany(u => u.Collections)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Likes)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Collection>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Collection)
                .HasForeignKey(i => i.CollectionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Collection>()
                .HasMany(c => c.Tags)
                .WithOne(t => t.Collection)
                .HasForeignKey(t => t.CollectionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Item>()
                .HasMany(i => i.Comments)
                .WithOne(c => c.Item)
                .HasForeignKey(c => c.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Item>()
                .HasMany(i => i.Likes)
                .WithOne(l => l.Item)
                .HasForeignKey(l => l.ItemId)
                .OnDelete(DeleteBehavior.Cascade);


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

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
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
                    CollectionId = Guid.Parse("22222222-2222-2222-2222-222222222222")
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
}
