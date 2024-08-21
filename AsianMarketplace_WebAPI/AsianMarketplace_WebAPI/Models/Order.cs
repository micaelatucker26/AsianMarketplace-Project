using System;
using System.Collections.Generic;

namespace AsianMarketplace_WebAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Username { get; set; } = null!;

        public virtual Shopper UsernameNavigation { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
