using PersonalCollectionManager.Application.DTOs.RequestDtos;

namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    public class AlgoliaItemDto
    {
        public string ObjectID { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid CollectionId { get; set; }
        public string CollectionName { get; set; }
        public string CollectionDescription { get; set; }
        public string CotegorieName { get; set; }
        public List<string> TagNames { get; set; } = new List<string>();
        public List<string> CustomFieldValues { get; set; } = new List<string>();
        public int Likes { get; set; }
        public List<string> Comments { get; set; } = new List<string>();
    }
}
