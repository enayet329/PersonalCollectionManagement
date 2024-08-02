using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalCollectionManager.Domain.Entities;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasIndex(i => i.Id).IsUnique();

        builder.HasMany(i => i.Tags)
            .WithOne(t => t.Item)
            .HasForeignKey(t => t.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(i => i.Comments)
            .WithOne(c => c.Item)
            .HasForeignKey(c => c.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(i => i.Likes)
            .WithOne(l => l.Item)
            .HasForeignKey(l => l.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(i => i.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
