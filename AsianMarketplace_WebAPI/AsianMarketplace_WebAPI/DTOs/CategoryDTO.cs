using System.ComponentModel.DataAnnotations;

namespace AsianMarketplace_WebAPI.DTOs
{
    public class CategoryDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
