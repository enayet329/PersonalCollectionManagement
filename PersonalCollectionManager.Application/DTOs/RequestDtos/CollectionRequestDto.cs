
using PersonalCollectionManager.Domain.Entities;
using System.Xml.Linq;

namespace PersonalCollectionManager.Application.DTOs.RequestDtos
{
    public class CollectionRequestDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Topic { get; set; }
        public string ImageUrl { get; set; }
        public Guid UserId { get; set; }
    }
}

