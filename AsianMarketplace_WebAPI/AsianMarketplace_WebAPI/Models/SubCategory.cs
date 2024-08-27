using System;
using System.Collections.Generic;

namespace AsianMarketplace_WebAPI.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Items = new HashSet<Item>();
        }

        public Guid SubCategoryId { get; set; }
        public string Name { get; set; } = null!;
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Item> Items { get; set; }
    }
}
