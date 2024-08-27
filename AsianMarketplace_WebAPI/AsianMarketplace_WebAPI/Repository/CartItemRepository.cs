using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Interfaces;
using AsianMarketplace_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Repository
{
    public class CartItemRepository : ICartItemRepo
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;
        public CartItemRepository(AsianMarketplaceDbContext context)
        {
            _marketplaceDbContext = context;
        }

        public async Task<CartItem> CreateCartItem(CartItem newCartItem)
        {
            await _marketplaceDbContext.CartItems.AddAsync(newCartItem);
            await _marketplaceDbContext.SaveChangesAsync();
            return newCartItem;
        }

        public async Task<CartItem> DeleteCartItem(Guid cartItemId)
        {
            var cartItem = await _marketplaceDbContext.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return null;
            }

            _marketplaceDbContext.Remove(cartItem);
            await _marketplaceDbContext.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem> GetCartItem( Guid cartItemId)
        {
            var cartItem = await _marketplaceDbContext.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return null;
            }
            return cartItem;
        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var cartItems = await _marketplaceDbContext.CartItems
                .ToListAsync();
            if(cartItems == null)
            {
                return null;
            }
            return cartItems;
        }

        public async Task<CartItem> UpdateCartItem(Guid cartItemId, CartItemDTO cartItemDTO)
        {
            var existingCartItem = await _marketplaceDbContext.CartItems.FindAsync(cartItemId);
            if(existingCartItem == null)
            {
                return null;
            }

            existingCartItem.Quantity = cartItemDTO.Quantity;

            await _marketplaceDbContext.SaveChangesAsync();
            return existingCartItem;
        }
    }
}
