namespace AsianMarketplace_WebAPI.DTOs
{
    public class ShoppingListDTO
    {
        public string Title { get; set; }
        public char IsActive { get; set; } = 'N';
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}