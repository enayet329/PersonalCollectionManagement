using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PersonalCollectionManager.Domain.Entities;

public class CustomFieldValueConfiguration : IEntityTypeConfiguration<CustomFieldValue>
{
    public void Configure(EntityTypeBuilder<CustomFieldValue> builder)
    {
        builder.HasKey(cfv => cfv.Id);

        builder.Property(cfv => cfv.Value)
            .IsRequired(false);

        builder.HasOne(cfv => cfv.CustomField)
            .WithMany(cf => cf.CustomFieldValues)
            .HasForeignKey(cfv => cfv.CustomFieldId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(cfv => cfv.Item)
            .WithMany(i => i.CustomFieldValues)
            .HasForeignKey(cfv => cfv.ItemId)
            .OnDelete(DeleteBehavior.NoAction); 
    }
}
