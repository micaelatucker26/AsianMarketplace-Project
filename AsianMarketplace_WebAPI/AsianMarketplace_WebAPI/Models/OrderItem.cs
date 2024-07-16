using System;
using System.Collections.Generic;

namespace AsianMarketplace_WebAPI.Models
{
    public partial class OrderItem
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid ItemId { get; set; }
        public Guid OrderId { get; set; }

        // These fields are virtual to allow EF Core to override
        // these properties in derived classes and to allow lazy loading
        // that delays loading their values from the db until accessed the
        // first time
        public virtual Item Item { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
