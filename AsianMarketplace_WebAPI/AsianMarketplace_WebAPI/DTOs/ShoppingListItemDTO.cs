using System.ComponentModel.DataAnnotations;

namespace AsianMarketplace_WebAPI.DTOs
{
    public class ShoppingListItemDTO
    {
        public string? IsCrossedOff { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string ShoppingListTitle { get; set; }
        [Required]
        public string ItemName { get; set; }
    }
}