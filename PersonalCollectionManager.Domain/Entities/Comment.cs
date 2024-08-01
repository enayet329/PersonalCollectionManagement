using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalCollectionManager.Domain.Entities
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
