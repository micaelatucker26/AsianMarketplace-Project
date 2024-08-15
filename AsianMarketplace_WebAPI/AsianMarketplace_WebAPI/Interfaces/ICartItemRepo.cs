using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;

namespace AsianMarketplace_WebAPI.Interfaces
{
    public interface ICartItemRepo
    {
        Task<List<CartItem>> GetCartItems();
        Task<CartItem> GetCartItem(Guid itemId, string userId);
        Task<CartItem> CreateCartItem(CartItem item);
        Task<CartItem> UpdateCartItem(Guid itemId,string userId, CartItemDTO cartItemDTO);
        Task<CartItem> DeleteCartItem(Guid itemId, string userId);
    }
}
