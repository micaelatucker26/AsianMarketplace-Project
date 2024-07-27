using AsianMarketplace_WebAPI.DTOs;

namespace AsianMarketplace_WebAPI.DTOs
{
    public class ItemDTO
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string SubCategoryName { get; set; }
    }
}