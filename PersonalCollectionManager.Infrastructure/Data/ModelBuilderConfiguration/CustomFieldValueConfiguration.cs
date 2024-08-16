using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Infrastructure.Data.ModelBuilderConfiguration
{
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
}
