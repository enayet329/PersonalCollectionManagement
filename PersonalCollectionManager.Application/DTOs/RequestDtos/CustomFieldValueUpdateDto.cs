using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Application.DTOs.RequestDtos
{
    public class CustomFieldValueUpdateDto
    {
        public Guid Id { get; set; }
        public string? Value { get; set; }
        public Guid CustomFieldId { get; set; }
        public Guid ItemId { get; set; }
    }
}
