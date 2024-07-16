using System;
using System.Collections.Generic;

namespace AsianMarketplace_WebAPI.Models
{
    public partial class ShoppingListView
    {
        public string ItemName { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int Quantity { get; set; }
        public string? IsCrossedOff { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Title { get; set; } = null!;
    }
}
