using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;

namespace AsianMarketplace_WebAPI.Interfaces
{
    public interface ICartItemRepo
    {
        Task<List<CartItem>> GetCartItems();
        Task<CartItem> GetCartItem(Guid cartItemId);
        Task<CartItem> CreateCartItem(CartItem cartItem);
        Task<CartItem> UpdateCartItem(Guid itemId, Guid userId, CartItem cartItem);
        Task<CartItem> RemoveCartItemFromCart(CartItem cartItem);
        Task<CartItem> DeleteRecord(Guid cartItemId);
        Task<CartItem> GetCartItemByItemAndUser(Guid itemId, Guid userId);
    }
}
