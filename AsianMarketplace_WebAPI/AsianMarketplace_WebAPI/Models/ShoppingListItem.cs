using System;
using System.Collections.Generic;

namespace AsianMarketplace_WebAPI.Models
{
    public partial class ShoppingListItem
    {
        public string? IsCrossedOff { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public Guid ItemId { get; set; }

        // These fields are virtual to allow EF Core to override
        // these properties in derived classes and to allow lazy loading
        // that delays loading their values from the db until accessed the
        // first time
        public virtual Item Item { get; set; } = null!;
        public virtual ShoppingList ShoppingList { get; set; } = null!;
    }
}
