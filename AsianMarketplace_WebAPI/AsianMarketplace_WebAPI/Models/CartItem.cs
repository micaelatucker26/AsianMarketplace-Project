using System;
using System.Collections.Generic;

namespace AsianMarketplace_WebAPI.Models
{
    public partial class CartItem
    {
        public int Quantity { get; set; }
        public Guid ItemId { get; set; }
        public string UserId { get; set; } = null!;

        // These fields are virtual to allow EF Core to override
        // these properties in derived classes and to allow lazy loading
        // that delays loading their values from the db until accessed the
        // first time
        public virtual Item Item { get; set; } = null!;
        public virtual Shopper User { get; set; } = null!;
    }
}
