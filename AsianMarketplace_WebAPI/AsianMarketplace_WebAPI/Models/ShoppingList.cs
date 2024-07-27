using System;
using System.Collections.Generic;

namespace AsianMarketplace_WebAPI.Models
{
    public partial class ShoppingList
    {
        public ShoppingList()
        {
            ShoppingListItems = new HashSet<ShoppingListItem>();
        }

        public string Title { get; set; } = null!;
        public char IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; } = null!;

        // These fields are virtual to allow EF Core to override
        // these properties in derived classes and to allow lazy loading
        // that delays loading their values from the db until accessed the
        // first time
        public virtual Shopper User { get; set; } = null!;
        public virtual ICollection<ShoppingListItem> ShoppingListItems { get; set; }
    }
}
