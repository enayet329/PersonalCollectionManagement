using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PersonalCollectionManager.Domain.Entities;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasIndex(i => i.Id).IsUnique();

        builder.Property(i => i.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(i => i.Collection)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(i => i.Comments)
            .WithOne(c => c.Item)
            .HasForeignKey(c => c.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(i => i.Likes)
            .WithOne(l => l.Item)
            .HasForeignKey(l => l.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(i => i.CustomFieldValues)
            .WithOne(cfv => cfv.Item)
            .HasForeignKey(cfv => cfv.ItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
