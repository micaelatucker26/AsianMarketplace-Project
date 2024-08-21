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

        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}
