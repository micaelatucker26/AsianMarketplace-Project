using System;
using System.Collections.Generic;

namespace AsianMarketplace_WebAPI.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public string Name { get; set; } = null!;

        // This fields is virtual to allow EF Core to override
        // this properties in derived classes and to allow lazy loading
        // that delays loading its values from the db until accessed the
        // first time
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
