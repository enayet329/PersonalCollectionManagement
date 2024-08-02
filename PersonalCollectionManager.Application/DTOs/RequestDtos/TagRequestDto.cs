
using PersonalCollectionManager.Application.DTOs.ResponseDtos;

namespace PersonalCollectionManager.Application.DTOs.RequestDtos
{
    public class TagRequestDto
    {
        public string Name { get; set; }

        public Guid ItemId { get; set; }
    }
}
