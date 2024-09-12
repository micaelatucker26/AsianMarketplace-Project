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

        public async Task<CartItem> RemoveCartItemFromCart(CartItem cartItem)
        {
            var cartItemToRemove = await _marketplaceDbContext.CartItems.FindAsync(cartItem.CartItemId);
            _marketplaceDbContext.CartItems.Remove(cartItemToRemove);
            await _marketplaceDbContext.SaveChangesAsync();
            return cartItemToRemove;
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

        public async Task<CartItem> GetCartItemByItemAndUser(Guid itemId, Guid userId)
        {
            var cartItem = await _marketplaceDbContext.CartItems
                .FirstOrDefaultAsync(ci => ci.ItemId == itemId && ci.UserId == userId);
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

        public async Task<CartItem> UpdateCartItem(Guid itemId, Guid userId, CartItem cartItem)
        {
            var existingCartItem = await GetCartItemByItemAndUser(itemId, userId);
            if(existingCartItem == null)
            {
                return null;
            }
            existingCartItem.Quantity = cartItem.Quantity;
            await _marketplaceDbContext.SaveChangesAsync();
            return existingCartItem;
        }

        public async Task<CartItem> DeleteRecord(Guid cartItemId)
        {
            var recordToDelete = await _marketplaceDbContext.CartItems.FindAsync(cartItemId);
            if (recordToDelete == null)
            {
                return null;
            }
            _marketplaceDbContext.Remove(recordToDelete);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();

            return recordToDelete;
        }
    }
}
