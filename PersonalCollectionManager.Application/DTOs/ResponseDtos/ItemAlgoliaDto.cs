namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    public class ItemAlgoliaDto
    {
        public string ObjectID { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public string CollectionName { get; set; }
        public Guid CollectionId { get; set; }
        public List<string> TagNames { get; set; }
        public int Likes { get; set; }
    }
}
