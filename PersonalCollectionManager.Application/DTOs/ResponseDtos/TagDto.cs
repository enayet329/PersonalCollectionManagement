using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    public class TagDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CollectionId { get; set; }
        public IEnumerable<CollectionDTO> Collection { get; set; }
    }
}
