namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    public class TagDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ItemDTO> Items { get; set; }
    }
}
