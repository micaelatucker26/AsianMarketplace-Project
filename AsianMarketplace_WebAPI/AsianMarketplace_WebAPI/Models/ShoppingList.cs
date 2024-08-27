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

        public Guid ShoppingListId { get; set; }
        public string Title { get; set; } = null!;
        public string IsActive { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public Guid UserId { get; set; }

        public virtual Shopper User { get; set; } = null!;
        public virtual ICollection<ShoppingListItem> ShoppingListItems { get; set; }
    }
}
