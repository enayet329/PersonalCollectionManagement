

namespace PersonalCollectionManager.Application.DTOs.RequestDtos
{
    public class RegisterRequestDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
