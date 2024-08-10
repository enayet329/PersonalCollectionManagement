namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        public string UserName { get; set; }
        public string UserProfileImgeUrl { get; set; }
    }
}
