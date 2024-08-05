
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PersonalCollectionManager.Domain.Entities
{
    public class CustomField
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FieldType { get; set; }

        [Required]
        public Guid CollectionId { get; set; }

        [ForeignKey(nameof(CollectionId))]
        public virtual Collection Collection { get; set; }

        public virtual ICollection<CustomFieldValue> CustomFieldValues { get; set; }
    }

}
