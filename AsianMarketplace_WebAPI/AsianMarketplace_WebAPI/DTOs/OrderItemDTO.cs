using System.ComponentModel.DataAnnotations;

namespace AsianMarketplace_WebAPI.DTOs
{
    public class OrderItemDTO
    {
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public Guid ItemId { get; set; }
        [Required]
        public Guid OrderId { get; set; }
    }
}