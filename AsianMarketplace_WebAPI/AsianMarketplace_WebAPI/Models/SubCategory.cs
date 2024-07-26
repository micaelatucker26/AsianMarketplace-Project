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

        // These fields are virtual to allow EF Core to override
        // these properties in derived classes and to allow lazy loading
        // that delays loading their values from the db until accessed the
        // first time
        public virtual Category CategoryNameNavigation { get; set; } = null!;
        public virtual ICollection<Item> Items { get; set; }
    }
}
