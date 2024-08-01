namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    public class CollectionDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Topic { get; set; }
        public string ImageUrl { get; set; }
        public Guid UserId { get; set; }
        public UserDTO User { get; set; }
        public IEnumerable<ItemDTO> Items { get; set; }
    }
}
