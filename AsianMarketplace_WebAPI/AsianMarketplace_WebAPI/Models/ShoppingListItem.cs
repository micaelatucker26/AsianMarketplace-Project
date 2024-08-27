using System;
using System.Collections.Generic;

namespace AsianMarketplace_WebAPI.Models
{
    public partial class ShoppingListItem
    {
        public Guid ShoppingListItemId { get; set; }
        public string? IsCrossedOff { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; } = null!;
        public Guid ItemId { get; set; }
        public Guid ShoppingListId { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual ShoppingList ShoppingList { get; set; } = null!;
    }
}
