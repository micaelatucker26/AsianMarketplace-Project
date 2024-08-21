using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AsianMarketplace_WebAPI.Models
{
    public partial class Category
    {
        [Key] // Use this attribute to mark the primary key
        public int CategoryID { get; set; } // Primary key
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public string Name { get; set; } = null!;

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
