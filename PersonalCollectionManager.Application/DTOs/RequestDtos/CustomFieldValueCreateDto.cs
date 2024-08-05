using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Application.DTOs.RequestDtos
{
    public class CustomFieldValueCreateDto
    {
        public string Value { get; set; }
        public Guid CustomFieldId { get; set; }
        public Guid ItemId { get; set; }
    }

}
