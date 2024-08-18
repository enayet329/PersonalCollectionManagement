
namespace PersonalCollectionManager.Application.DTOs.RequestDtos
{
    public class CollectionUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string topic { get; set; }
        public string imageUrl { get; set; }
        public Guid UserId { get; set; }
    }
}
