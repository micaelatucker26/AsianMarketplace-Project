namespace AsianMarketplace_WebAPI.DTOs
{
    public class CartItemDTO
    {
        public int Quantity { get; set; }
        public Guid ItemId { get; set; }
        public string UserId { get; set; }
    }
}
