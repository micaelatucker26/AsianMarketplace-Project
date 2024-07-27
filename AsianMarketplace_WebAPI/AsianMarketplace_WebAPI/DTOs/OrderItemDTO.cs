namespace AsianMarketplace_WebAPI.DTOs
{
    public class OrderItemDTO
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid ItemId { get; set; }
        public Guid OrderId { get; set; }
    }
}