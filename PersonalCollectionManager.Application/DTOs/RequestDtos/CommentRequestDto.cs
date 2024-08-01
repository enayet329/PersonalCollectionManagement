
using PersonalCollectionManager.Application.DTOs.ResponseDtos;

namespace PersonalCollectionManager.Application.DTOs.RequestDtos
{
    public class CommentRequestDto
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }

    }
}
