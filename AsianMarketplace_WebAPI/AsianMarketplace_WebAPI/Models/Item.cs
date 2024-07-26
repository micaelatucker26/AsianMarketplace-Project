using System;
using System.Collections.Generic;

namespace AsianMarketplace_WebAPI.Models
{
    public partial class Item
    {
        public Item()
        {
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
            ShoppingListItems = new HashSet<ShoppingListItem>();
        }

        public Guid ItemId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string SubCategoryName { get; set; } = null!;

        // These fields are virtual to allow EF Core to override
        // these properties in derived classes and to allow lazy loading
        // that delays loading their values from the db until accessed the
        // first time
        public virtual SubCategory SubCategoryNameNavigation { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ShoppingListItem> ShoppingListItems { get; set; }
    }
}
