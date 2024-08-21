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

        public virtual Item Item { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
