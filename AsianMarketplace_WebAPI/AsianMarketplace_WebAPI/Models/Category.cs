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
        public int CategoryId { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
