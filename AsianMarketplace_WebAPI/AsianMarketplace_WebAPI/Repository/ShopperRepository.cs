using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Interfaces;
using AsianMarketplace_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Repository
{
    public class ShopperRepository : IShopperRepo
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;
        public ShopperRepository(AsianMarketplaceDbContext context)
        {
            _marketplaceDbContext = context;
        }

        public async Task<Shopper> CreateShopper(Shopper shopper)
        {
            // Add the new category to the context
            await _marketplaceDbContext.Shoppers.AddAsync(shopper);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();
            return shopper;
        }

        public async Task<Shopper> DeleteShopper(string name)
        {
            var shopper = await _marketplaceDbContext.Shoppers.FirstOrDefaultAsync(s => s.Username == name);
            if (shopper == null)
            {
                return null;
            }
            // Remove the category from the database
            _marketplaceDbContext.Shoppers.Remove(shopper);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();
            return shopper;
        }

        public async Task<Shopper> GetShopper(string name)
        {
            var shopper = await _marketplaceDbContext.Shoppers.FirstOrDefaultAsync(c => c.Username == name);
            if (shopper == null)
            {
                return null;
            }
            return shopper;
        }

        public async Task<List<Shopper>> GetShoppers()
        {
            var shoppers = await _marketplaceDbContext.Shoppers.ToListAsync();
            if (shoppers == null)
            {
                return null;
            }
            return shoppers;
        }

        public async Task<Shopper> UpdateShopper(string name, ShopperDTO shopperDTO)
        {
            var existingShopper = await _marketplaceDbContext.Shoppers.FirstOrDefaultAsync(c => c.Username == name);

            if (existingShopper == null)
            {
                return null;
            }
            var usernameExists = await _marketplaceDbContext.Shoppers.AnyAsync(c => c.Username == shopperDTO.Username && c.UserId != existingShopper.UserId);
            if(usernameExists)
            {
                return null;
            }
            // The shopper name will be updated
            existingShopper.Username = shopperDTO.Username;
            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();
            return existingShopper;
        }
    }
}
