using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PersonalCollectionManager.Domain.Entities;

public class CustomFieldConfiguration : IEntityTypeConfiguration<CustomField>
{
    public void Configure(EntityTypeBuilder<CustomField> builder)
    {
        builder.HasKey(cf => cf.Id);

        builder.HasIndex(cf => cf.Id).IsUnique();

        builder.Property(cf => cf.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(cf => cf.FieldType)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasOne(cf => cf.Collection)
            .WithMany(c => c.CustomFields)
            .HasForeignKey(cf => cf.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(cf => cf.CustomFieldValues)
            .WithOne(cfv => cfv.CustomField)
            .HasForeignKey(cfv => cfv.CustomFieldId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
