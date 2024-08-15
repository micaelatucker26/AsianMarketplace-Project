using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Interfaces;
using AsianMarketplace_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Repository
{
    public class CategoryRepository : ICategoryRepo
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;

        public CategoryRepository(AsianMarketplaceDbContext context)
        {
            _marketplaceDbContext = context;
        }
        public async Task<Category> CreateCategory(Category category)
        {
            // Add the new category to the context
            await _marketplaceDbContext.Categories.AddAsync(category);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> DeleteCategory(string name)
        {
            var category = await _marketplaceDbContext.Categories.FindAsync(name);
            // Remove the category from the database
            _marketplaceDbContext.Categories.Remove(category);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();
            return category;

        }

        public async Task<List<Category>> GetCategories()
        {
           return await _marketplaceDbContext.Categories.ToListAsync(); 
        }

        public async Task<Category> GetCategory(string name)
        {
            return await _marketplaceDbContext.Categories.FindAsync(name);
        }

        //public async Task<Category> UpdateCategory(string name, CategoryDTO categoryDTO)
        //{
            //var existingCategory = await _marketplaceDbContext.Categories.FindAsync(name);

            //// The category name will be updated
            //existingCategory.Name = categoryDTO.Name;

            //// Save changes to the database
            //await _marketplaceDbContext.SaveChangesAsync();
            //return existingCategory;
        //}
    }
}
