using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;

namespace AsianMarketplace_WebAPI.Interfaces
{
    public interface ICartItemRepo
    {
        Task<List<CartItem>> GetCartItems();
        Task<CartItem> GetCartItem(Guid cartItemId);
        Task<CartItem> CreateCartItem(CartItem item);
        Task<CartItem> UpdateCartItem(Guid cartItemId, CartItemDTO cartItemDTO);
        Task<CartItem> DeleteCartItem(Guid cartItemId);
    }
}
