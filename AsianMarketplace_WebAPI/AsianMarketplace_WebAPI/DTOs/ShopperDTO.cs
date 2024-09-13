using AsianMarketplace_WebAPI.DTOs;
using System.ComponentModel.DataAnnotations;

namespace AsianMarketplace_WebAPI.DTOs
{
    public class ShopperDTO
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
