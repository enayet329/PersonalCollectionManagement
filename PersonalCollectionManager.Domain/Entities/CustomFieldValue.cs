using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PersonalCollectionManager.Domain.Entities
{
    public class CustomFieldValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? Value { get; set; }

        [Required]
        public Guid CustomFieldId { get; set; }

        [ForeignKey(nameof(CustomFieldId))]
        public virtual CustomField CustomField { get; set; }

        [Required]
        public Guid ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual Item Item { get; set; }
    }
}
