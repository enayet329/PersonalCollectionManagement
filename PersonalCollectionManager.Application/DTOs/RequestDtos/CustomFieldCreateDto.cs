using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Application.DTOs.RequestDtos
{
    public class CustomFieldCreateDto
    {
        public string Name { get; set; }
        public string FieldType { get; set; }
        public Guid CollectionId { get; set; }
    }

}
