namespace AsianMarketplace_WebAPI.DTOs
{
    public class ShoppingListItemDTO
    {
        public string? IsCrossedOff { get; set; }   
        public int Quantity { get; set; }
        public string ShoppingListTitle { get; set; }
        public string ItemName { get; set; }
    }
}