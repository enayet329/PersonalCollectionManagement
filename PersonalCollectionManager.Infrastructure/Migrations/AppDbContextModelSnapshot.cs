﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalCollectionManager.Infrastructure.Data;

#nullable disable

namespace PersonalCollectionManager.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.Collection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Collections");

                    b.HasData(
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            Description = "First collection",
                            ImageUrl = "https://example.com/collection1.jpg",
                            Name = "Collection 1",
                            Topic = "General",
                            UserId = new Guid("11111111-1111-1111-1111-111111111111")
                        });
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("55555555-5555-5555-5555-555555555555"),
                            Content = "First comment",
                            CreatedAt = new DateTime(2024, 8, 3, 13, 2, 13, 308, DateTimeKind.Utc).AddTicks(49),
                            ItemId = new Guid("33333333-3333-3333-3333-333333333333"),
                            UserId = new Guid("11111111-1111-1111-1111-111111111111")
                        });
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CollectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = new Guid("33333333-3333-3333-3333-333333333333"),
                            CollectionId = new Guid("22222222-2222-2222-2222-222222222222"),
                            DateAdded = new DateTime(2024, 8, 3, 13, 2, 13, 307, DateTimeKind.Utc).AddTicks(9926),
                            Description = "First item",
                            ImgUrl = "https://example.com/item1.jpg",
                            Name = "Item 1"
                        });
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.ItemTag", b =>
                {
                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ItemId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ItemTags");

                    b.HasData(
                        new
                        {
                            ItemId = new Guid("33333333-3333-3333-3333-333333333333"),
                            TagId = new Guid("44444444-4444-4444-4444-444444444444")
                        });
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.Like", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId", "ItemId")
                        .IsUnique();

                    b.ToTable("Likes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("66666666-6666-6666-6666-666666666666"),
                            ItemId = new Guid("33333333-3333-3333-3333-333333333333"),
                            UserId = new Guid("11111111-1111-1111-1111-111111111111")
                        });
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");

                    b.HasData(
                        new
                        {
                            Id = new Guid("77777777-7777-7777-7777-777777777777"),
                            Created = new DateTime(2024, 8, 3, 13, 2, 13, 308, DateTimeKind.Utc).AddTicks(147),
                            Expires = new DateTime(2024, 8, 10, 13, 2, 13, 308, DateTimeKind.Utc).AddTicks(140),
                            Token = "sampleRefreshToken1",
                            UserId = new Guid("11111111-1111-1111-1111-111111111111")
                        },
                        new
                        {
                            Id = new Guid("88888888-8888-8888-8888-888888888888"),
                            Created = new DateTime(2024, 8, 3, 13, 2, 13, 308, DateTimeKind.Utc).AddTicks(150),
                            Expires = new DateTime(2024, 8, 10, 13, 2, 13, 308, DateTimeKind.Utc).AddTicks(149),
                            Token = "sampleRefreshToken2",
                            UserId = new Guid("d2c6e7b4-4a76-4b1e-8d8f-2b9f2f7e0e77")
                        });
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = new Guid("44444444-4444-4444-4444-444444444444"),
                            Name = "Tag 1"
                        });
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreferredLanguage")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<bool>("PreferredThemeDark")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d2c6e7b4-4a76-4b1e-8d8f-2b9f2f7e0e77"),
                            Email = "admin@example.com",
                            ImageURL = "https://example.com/admin.jpg",
                            IsAdmin = true,
                            IsBlocked = false,
                            PasswordHash = "$2a$10$tZt6npOCgGVoeWtoihCoMOn3cM2GSfpPCS.dAfVYM6x4voeU1uNwO",
                            PreferredLanguage = "en",
                            PreferredThemeDark = false,
                            Username = "admin"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            Email = "user1@example.com",
                            ImageURL = "https://example.com/user1.jpg",
                            IsAdmin = false,
                            IsBlocked = false,
                            PasswordHash = "$2a$10$irUycdjwCU.yXpW/wzfbmuUIZEnHuqkKz7VKZ2DEeFuDl4aUmTZ7e",
                            PreferredLanguage = "en",
                            PreferredThemeDark = false,
                            Username = "user1"
                        });
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.Collection", b =>
                {
                    b.HasOne("PersonalCollectionManager.Domain.Entities.User", "User")
                        .WithMany("Collections")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.Comment", b =>
                {
                    b.HasOne("PersonalCollectionManager.Domain.Entities.Item", "Item")
                        .WithMany("Comments")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalCollectionManager.Domain.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.Item", b =>
                {
                    b.HasOne("PersonalCollectionManager.Domain.Entities.Collection", "Collection")
                        .WithMany("Items")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.ItemTag", b =>
                {
                    b.HasOne("PersonalCollectionManager.Domain.Entities.Item", "Item")
                        .WithMany("ItemTags")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalCollectionManager.Domain.Entities.Tag", "Tag")
                        .WithMany("ItemTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.Like", b =>
                {
                    b.HasOne("PersonalCollectionManager.Domain.Entities.Item", "Item")
                        .WithMany("Likes")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalCollectionManager.Domain.Entities.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("PersonalCollectionManager.Domain.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.Collection", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.Item", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ItemTags");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.Tag", b =>
                {
                    b.Navigation("ItemTags");
                });

            modelBuilder.Entity("PersonalCollectionManager.Domain.Entities.User", b =>
                {
                    b.Navigation("Collections");

                    b.Navigation("Comments");

                    b.Navigation("Likes");

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
