

namespace PersonalCollectionManager.Application.DTOs.RequestDtos
{
    public class ItemUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
    }
}
