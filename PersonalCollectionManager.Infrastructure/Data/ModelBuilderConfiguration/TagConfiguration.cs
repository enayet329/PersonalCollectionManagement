using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalCollectionManager.Domain.Entities;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasIndex(t => t.Id).IsUnique();

        builder.Property(t => t.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasOne(t => t.Item)
            .WithMany(i => i.Tags)
            .HasForeignKey(t => t.ItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
