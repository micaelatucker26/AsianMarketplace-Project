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

        public string Name { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public virtual ICollection<Item> Items { get; set; }
    }
}
