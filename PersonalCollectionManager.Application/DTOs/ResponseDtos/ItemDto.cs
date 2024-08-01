namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    public class ItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public Guid CollectionId { get; set; }
        public CollectionDTO Collection { get; set; }
        public IEnumerable<TagDTO> Tags { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
        public IEnumerable<LikeDTO> Likes { get; set; }
    }
}
