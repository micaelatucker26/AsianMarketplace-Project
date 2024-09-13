namespace AsianMarketplace_WebAPI.DTOs
{
    public class CartItemDTO
    {
        public Guid ItemId { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
    }
}
