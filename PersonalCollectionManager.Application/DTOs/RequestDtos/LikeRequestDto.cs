
using PersonalCollectionManager.Application.DTOs.ResponseDtos;

namespace PersonalCollectionManager.Application.DTOs.RequestDtos
{
    public class LikeRequestDto
    {
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }

    }
}
