using System.ComponentModel.DataAnnotations;

namespace AsianMarketplace_WebAPI.DTOs
{
    public class SubCategoryDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
