using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    public class CustomFieldValueDto
    {
        public Guid Id { get; set; }
        public string? Value { get; set; }
        public Guid CustomFieldId { get; set; }
        public Guid ItemId { get; set; }
        public string? customFieldName { get; set; }
        public string? fieldType { get; set; }
    }
}
