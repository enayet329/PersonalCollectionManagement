using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalCollectionManager.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ImageURL { get; set; }

        [StringLength(3)]
        public string PreferredLanguage { get; set; } = "en"; // Default to English
        public bool PreferredThemeDark { get; set; } = false; // Default to light theme
        public bool IsAdmin { get; set; } = false;
        public bool IsBlocked { get; set; } = false;

        public DateTime JoinedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
