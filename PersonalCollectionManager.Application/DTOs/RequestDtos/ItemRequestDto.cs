
using PersonalCollectionManager.Application.DTOs.ResponseDtos;

namespace PersonalCollectionManager.Application.DTOs.RequestDtos
{
    public class ItemRequestDto
    {
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public Guid CollectionId { get; set; }

    }
}
