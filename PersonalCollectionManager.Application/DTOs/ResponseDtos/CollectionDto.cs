using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    public class CollectionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Topic { get; set; }
        public string ImageUrl { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int ItemCount { get; set; }
    }
}
