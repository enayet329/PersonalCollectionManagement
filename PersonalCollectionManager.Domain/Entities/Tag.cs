using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalCollectionManager.Domain.Entities
{
    public class Tag
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
