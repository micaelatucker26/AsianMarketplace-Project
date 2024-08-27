using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Interfaces;
using AsianMarketplace_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Repository
{
    public class ItemRepository : IItemRepo
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;

        public ItemRepository(AsianMarketplaceDbContext context)
        {
            _marketplaceDbContext = context;
        }
        public async Task<Item> CreateItem(Item newItem)
        {
            await _marketplaceDbContext.Items.AddAsync(newItem);
            await _marketplaceDbContext.SaveChangesAsync();
            return newItem;
        }

        public async Task<Item> DeleteItem(Guid itemId)
        {
            var item = await _marketplaceDbContext.Items.FindAsync(itemId);
            if (item == null)
            {
                return null;
            }

            _marketplaceDbContext.Remove(item);
            await _marketplaceDbContext.SaveChangesAsync();
            return item;
        }

        public async Task<Item> GetItem(Guid itemId)
        {
            var item = await _marketplaceDbContext.Items.FindAsync(itemId);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        public async Task<List<Item>> GetItems()
        {
            var items = await _marketplaceDbContext.Items.ToListAsync();
            if (items == null)
            {
                return null;
            }
            return items;
        }

        public async Task<Item> UpdateItem(Guid itemId, ItemDTO itemDTO)
        {
            var existingItem = await _marketplaceDbContext.Items.FindAsync(itemId);
            if (existingItem == null)
            {
                return null;
            }

            existingItem.Name = itemDTO.Name;
            existingItem.Description = itemDTO.Description;
            existingItem.Price = itemDTO.Price;
            existingItem.Quantity = itemDTO.Quantity;
            existingItem.ImageUrl = itemDTO.ImageUrl;
            existingItem.SubCategory.Name = itemDTO.SubCategoryName;

            await _marketplaceDbContext.SaveChangesAsync();
            return existingItem;
        }
    }
}
