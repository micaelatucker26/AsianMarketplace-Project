using System;
using System.Collections.Generic;

namespace AsianMarketplace_WebAPI.Models
{
    public partial class Shopper
    {
        public Shopper()
        {
            CartItems = new HashSet<CartItem>();
            Orders = new HashSet<Order>();
            ShoppingLists = new HashSet<ShoppingList>();
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;

        // These fields are virtual to allow EF Core to override
        // these properties in derived classes and to allow lazy loading
        // that delays loading their values from the db until accessed the
        // first time
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}
