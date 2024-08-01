namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
        public IEnumerable<CollectionDTO> Collections { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
        public IEnumerable<LikeDTO> Likes { get; set; }
    }
}
