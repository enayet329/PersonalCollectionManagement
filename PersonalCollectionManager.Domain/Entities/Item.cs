using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalCollectionManager.Domain.Entities
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string ImgUrl { get; set; }

        [Required]
        public Guid CollectionId { get; set; }

        [ForeignKey("CollectionId")]
        public virtual Collection Collection { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
