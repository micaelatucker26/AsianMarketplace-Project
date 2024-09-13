using System.ComponentModel.DataAnnotations;

namespace AsianMarketplace_WebAPI.DTOs
{
    public class ShoppingListDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public char IsActive { get; set; } = 'N';
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}