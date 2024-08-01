namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    public class LikeDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserDTO User { get; set; }
        public Guid ItemId { get; set; }
        public ItemDTO Item { get; set; }
    }
}
