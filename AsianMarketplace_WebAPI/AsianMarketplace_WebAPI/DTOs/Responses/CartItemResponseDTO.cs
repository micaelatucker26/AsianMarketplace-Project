namespace AsianMarketplace_WebAPI.DTOs.Responses
{
    public class CartItemResponseDTO
    {
        public Guid ItemId { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
    }
}