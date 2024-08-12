namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ImageURL { get; set; }
        public string PrefrredLanguage { get; set; }
        public bool PreffrredThemeDark { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
    }
}
