using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Domain.Entities
{
    public class ItemTag
    {
        public Guid ItemId { get; set; }
        public virtual Item Item { get; set; }

        public Guid TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
