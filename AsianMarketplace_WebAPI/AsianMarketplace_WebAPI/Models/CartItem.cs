using System;
using System.Collections.Generic;

namespace AsianMarketplace_WebAPI.Models
{
    public partial class CartItem
    {
        public int Quantity { get; set; }
        public Guid ItemId { get; set; }
        public string UserId { get; set; } = null!;

        public virtual Item Item { get; set; } = null!;
        public virtual Shopper User { get; set; } = null!;
    }
}
