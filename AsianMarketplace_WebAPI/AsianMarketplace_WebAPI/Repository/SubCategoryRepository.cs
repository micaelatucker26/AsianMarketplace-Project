using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Interfaces;
using AsianMarketplace_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Repository
{
    public class SubCategoryRepository : ISubCategoryRepo
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;

        public SubCategoryRepository(AsianMarketplaceDbContext context)
        {
            _marketplaceDbContext = context;
        }
        public async Task<SubCategory> CreateSubCategory(SubCategory subCategory)
        {
            // Add the new subcategory to the context
            await _marketplaceDbContext.SubCategories.AddAsync(subCategory);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();
            return subCategory;
        }

        public async Task<SubCategory> DeleteSubCategory(string name)
        {
            var subCategory = await _marketplaceDbContext.SubCategories.FirstOrDefaultAsync(sc => sc.Name == name);
            if (subCategory == null)
            {
                return null;
            }
            // Remove that subcategory from the database
            _marketplaceDbContext.SubCategories.Remove(subCategory);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();
            return subCategory;
        }

        public async Task<List<SubCategory>> GetSubCategories()
        {
            var subCategories = await _marketplaceDbContext.SubCategories
                .Include(s => s.Items)
                .Include(s => s.Category)
                .ToListAsync();
            if (subCategories == null)
            {
                return null;
            }
            return subCategories;
        }

        public async Task<SubCategory> GetSubCategory(string name)
        {
            var subCategory = await _marketplaceDbContext.SubCategories.Include(sc => sc.Category).FirstOrDefaultAsync(sc => sc.Name == name);
            if (subCategory == null)
            {
                return null;
            }
            return subCategory;
        }

        public async Task<SubCategory> UpdateSubCategory(string name, SubCategoryDTO subCategoryDTO)
        {
            var existingSubCategory = await _marketplaceDbContext.SubCategories
                .Include(sc => sc.Category)
                .FirstOrDefaultAsync(sc => sc.Name == name);
            if (existingSubCategory == null)
            {
                return null;
            }
            // The name and category name will be updated
            existingSubCategory.Name = subCategoryDTO.Name;
            existingSubCategory.Category.Name = subCategoryDTO.CategoryName;

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();
            return existingSubCategory;
        }
    }
}
