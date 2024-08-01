using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalCollectionManager.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsBlocked { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
