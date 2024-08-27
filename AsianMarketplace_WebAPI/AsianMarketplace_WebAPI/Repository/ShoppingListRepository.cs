using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Interfaces;
using AsianMarketplace_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace AsianMarketplace_WebAPI.Repository
{
    public class ShoppingListRepository : IShoppingListRepo
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;
        public ShoppingListRepository(AsianMarketplaceDbContext context)
        {
            _marketplaceDbContext = context;
        }

        public async Task<ShoppingList> CreateShoppingList(ShoppingList shoppingList)
        {
            // Add the new shopping list to the context
            await _marketplaceDbContext.ShoppingLists.AddAsync(shoppingList);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();
            return shoppingList;
        }

        public async Task<ShoppingList> DeleteShoppingList(string title, Guid userId)
        {
            var shoppingList = await _marketplaceDbContext.ShoppingLists.FirstOrDefaultAsync( sl => sl.Title == title && sl.User.UserId == userId);
                //.FirstOrDefaultAsync(s => s.Title == title && s == userId);
            if (shoppingList == null)
            {
                return null;
            }
            // Remove the shopping list from the database
            _marketplaceDbContext.ShoppingLists.Remove(shoppingList);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();
            return shoppingList;
        }

        public async Task<ShoppingList> GetShoppingList(string title, Guid userId)
        {
            var shoppingList = await _marketplaceDbContext.ShoppingLists.FindAsync(title, userId);
            if (shoppingList == null)
            {
                return null;
            }
            return shoppingList;
        }

        public async Task<List<ShoppingList>> GetShoppingLists()
        {
            var shoppingLists = await _marketplaceDbContext.ShoppingLists.ToListAsync();
            if (shoppingLists == null)
            {
                return null;
            }
            return shoppingLists;
        }

        public async Task<ShoppingList> UpdateShoppingList(string title, Guid userId, ShoppingListDTO shoppingListDTO)
        {
            var existingShoppingList = await _marketplaceDbContext.ShoppingLists.FindAsync(title, userId);

            if (existingShoppingList == null)
            {
                return null;
            }
            // The shopping list title will be updated
            existingShoppingList.Title = shoppingListDTO.Title;
            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();
            return existingShoppingList;
        }
    }
}
