using PersonalCollectionManager.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    public class ItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public Guid CollectionId { get; set; }
        public CollectionDTO Collection { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
        public IEnumerable<LikeDTO> Likes { get; set; }
    }
}
