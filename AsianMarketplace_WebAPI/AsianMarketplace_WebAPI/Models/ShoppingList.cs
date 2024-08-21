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

        public virtual Shopper User { get; set; } = null!;
        public virtual ICollection<ShoppingListItem> ShoppingListItems { get; set; }
    }
}
